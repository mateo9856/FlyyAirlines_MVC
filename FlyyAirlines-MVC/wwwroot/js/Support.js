let connection = null;

const connectedUsers = [];

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

setInterval(() => {
    if (connection != null) {
        connection.invoke("GetConnectedUsers")
            .then((res) => updateList(res))
            .catch(err => console.error(err));
    }

}, 2000);

sendEvent.addEventListener("click", (e => {
    e.preventDefault();
    const user = null;
    const message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message)
        .catch(err => console.error(err.toString));
}));

function updateList(element) {
    const GetList = document.getElementById("usersList");
    const GetSelect = document.getElementById("userSelect");
    element.forEach(el => {
        if (!connectedUsers.some(x => x.connectionId === el.connectionId)) {
            connectedUsers.push(el);
            const optionElement = document.createElement("option");
            const newList = document.createElement("li");

            newList.textContent = el.email;

            optionElement.appendChild(document.createTextNode(el.email))
            optionElement.value = el.connectionId;

            GetSelect.appendChild(optionElement);
            GetList.appendChild(newList);
        }
    });

}
