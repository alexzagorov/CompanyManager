let groupName = document.getElementById("taskName").innerHTML;

var connection =
    new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .build();

connection.on("NewMessage",
    function (message) {
        var chatInfo = `<div>[${message.user}] ${escapeHtml(message.text)}</div>`;
        $("#messagesList").append(chatInfo);
    });


$("#sendButton").click(function () {
    var message = $("#messageInput").val();
    connection.invoke("Send", message, groupName.toLowerCase());
});

connection.start()
    .then(function () {
        connection.invoke("AddToRoom", groupName.toLowerCase());
    })
    .catch(function (err) {
        return console.error(err.toString());
    });



function escapeHtml(unsafe) {
    return unsafe
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;")
};