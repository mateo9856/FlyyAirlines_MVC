let connection = null;

let connectedUsers = null;

connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build(); 

const sendEvent = document.getElementById("sendButton");

sendEvent.disabled = true;

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
    const user = null;
    //get connection id
    const message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message)
        .catch(err => console.error(err.toString));
}));