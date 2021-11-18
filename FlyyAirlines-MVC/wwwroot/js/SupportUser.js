let connection = null;

let userGroupName = "";

const connectedSupport = document.getElementById("connectedUser");
document.getElementById("reconnectBtn").display = "none";
connectedSupport.style.display = "none";
let connectedUsers = null;

connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build(); 

const sendEvent = document.getElementById("sendButton");

sendEvent.disabled = true;

let ActiveSupport = setInterval(() => {

    if (connection != null) {
        connection.invoke("GetActiveSupports")
            .then((res) => connectToSupport(res))
            .catch(err => console.error(err));
    }

}, 2000);

function connectToSupport(sup) {
    if (sup.length > 0) {
        const SelectUser = sup[Math.floor(Math.random() * sup.length)];
        connectedSupport.value = SelectUser.connectionId;

        document.getElementById("connectWait").style.display = "none";
        connectedSupport.style.display = "block";
        connectedSupport.textContent = "Połączono z doradcą";
        clearInterval(ActiveSupport);
        ActiveSupport = setInterval(checkConnection, 2000);
    }
}

async function checkConnection() {
    //wait to connect support and optimize this
    if (await !connection.invoke("CheckConnectionToSupport", connectedSupport.value, userGroupName)) {
        console.log("Działa");
        document.getElementById("connectWait").style.display = "block";
        document.getElementById("connectWait").textContent="Połączenie utracone połącz ponownie!"
        document.getElementById("reconnectBtn").display = "block";
        connectedSupport.style.display = "none";
        connectedSupport.value = null;
        connectedSupport.textContent = "";
        clearInterval(ActiveSupport);
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
    document.getElementById("connectWait").textContent = "Za chwile pomoc techniczna się z tobą skontaktuje...";
    document.getElementById("reconnectBtn").display = "none";
    ActiveSupport = setInterval(() => {

        if (connection != null) {
            connection.invoke("GetActiveSupports")
                .then((res) => connectToSupport(res))
                .catch(err => console.error(err));
        }

    }, 2000);
}