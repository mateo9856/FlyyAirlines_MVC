let connection = null;

connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build(); 

let userGroupName = "";

let connectStatus = "waitforconnect";

const connectedSupport = document.getElementById("connectedUser");

const connectWait = document.getElementById("connectWait");

const reconnectBtn = document.getElementById("reconnectBtn");

const sendEvent = document.getElementById("sendButton");

let connectedUsers = null;

sendEvent.disabled = true;

let ActiveSupport = setInterval(() => {

    if (connection != null) {
        connection.invoke("GroupPeopleCount", userGroupName)
            .then((res) => ConnectOperator(res))
            .catch(err => console.error(err));
    }

}, 2000);

function ConnectOperator(num) {
    if (connectStatus == "waitforconnect" && num == 2) {
        connectStatus = "connect";
        connection.invoke("GetSupportConnectionId", userGroupName).then(res => connectedSupport.value = res);
        changeStyle(connectStatus);
    }

    else if (connectStatus == "connect" && num == 1) {
        connectStatus = "disconnect";
        changeStyle(connectStatus);
        connectedSupport.value = "";
        clearInterval(ActiveSupport);
    }
}

function changeStyle(st) {
    switch (st) {
        case "waitforconnect":
            connectWait.textContent = "Za chwile pomoc techniczna się z tobą skontaktuje...";
            reconnectBtn.disabled = true;
            break;
        case "connect":
            connectWait.style.display = "none";
            connectedSupport.style.display = "block";
            connectedSupport.textContent = "Połączono z doradcą";
            break;
        case "disconnect":
            connectWait.style.display = "block";
            connectWait.textContent = "Połączenie utracone połącz ponownie!"
            reconnectBtn.disabled = false;
            connectedSupport.style.display = "none";
            connectedSupport.value = null;
            connectedSupport.textContent = "";
            break;
    }
}

connection.on("ReceiveMessage", function(user, message) {
    const li = document.createElement("li");
    li.innerHTML = `<b>${user}: </b> ${message}`;
    document.getElementById("messageList").appendChild(li);
});

connection.start().then(function() {
    sendEvent.disabled = false;
    connection.invoke("GetUserGroupName").then(res => userGroupName = res);
}).catch((err) => {
    return console.error(err.toString());
})

sendEvent.addEventListener("click", (e => {
    e.preventDefault();
    const user = connectedSupport.value;
    const message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message)
        .catch(err => console.error(err.toString));
}));

function reconnect() {
    connectStatus = "waitforconnect";
    changeStyle(connectStatus);
    document.getElementById("messageList").innerHTML = '';
    ActiveSupport = setInterval(() => {

        if (connection != null) {
            connection.invoke("GroupPeopleCount", userGroupName)
                .then((res) => ConnectOperator(res))
                .catch(err => console.error(err));
        }

    }, 2000);
}