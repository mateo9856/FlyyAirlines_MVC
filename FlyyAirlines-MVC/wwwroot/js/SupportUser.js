let connection = null;

const connectedSupport = document.getElementById("connectedUser");
connectedSupport.style.display = "none";
let connectedUsers = null;

connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build(); 

const sendEvent = document.getElementById("sendButton");

sendEvent.disabled = true;

const ActiveSupport = setInterval(() => {

    if (connection != null) {
        connection.invoke("GetActiveSupports")
            .then((res) => connectToSupport(res))
            .catch(err => console.error(err));
    }

}, 2000);

function connectToSupport(sup) {
    if (sup.length > 0) {
        const SelectUser = sup[Math.floor(Math.random() * sup.length)];
        console.log(SelectUser);
        connectedSupport.value = SelectUser.connectionId;

        document.getElementById("connectWait").style.display = "none";
        connectedSupport.style.display = "block";

        clearInterval(ActiveSupport);
    }
}

connection.on("ReceiveMessage", function (user, message) {
    const li = document.createElement("li");
    li.innerHTML = `<b>${user}: </b> ${message}`;
    document.getElementById("messageList").appendChild(li);
});

connection.start().then(function () {
    sendEvent.disabled = false;
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