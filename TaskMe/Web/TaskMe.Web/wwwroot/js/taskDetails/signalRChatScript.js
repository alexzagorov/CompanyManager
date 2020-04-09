let groupName = document.getElementById("taskId").innerHTML;
let userNames = document.getElementById("userNames").innerHTML;
let userPictureUrl = document.getElementById("userPictureUrl").innerHTML;

var connection =
    new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .build();

connection.on("NewMessage",
    function (object) {
        var chatInfo = `<div class="card">
            <div class="card-body">
                <h6 class="card-subtitle mb-2 text-muted text-left">${object.user} <img class="rounded-circle border-dark" src="${object.pictureUrl}" width="20" height="20" /></h6>
                <p class="card-text float-left bg-secondary rounded-pill">${escapeHtml(object.text)}</p>
            </div>
        </div>`
        $("#messagesList").append(chatInfo);
    });


$("#sendButton").click(function () {
    var message = $("#messageInput").val();
    connection.invoke("Send", message, groupName.toLowerCase());
    appendMyMsg(message);
    document.getElementById("messageInput").value = "";
});

connection.start()
    .then(function () {
        connection.invoke("AddToRoom", groupName.toLowerCase());
    })
    .catch(function (err) {
        return console.error(err.toString());
    });


function appendMyMsg(message) {
    var chatInfo = `<div class="card">
            <div class="card-body">
                <h6 class="card-subtitle mb-2 text-muted text-right">${userNames} <img class="rounded-circle border-dark" src="${userPictureUrl}" width="20" height="20" /></h6>
                <p class="card-text float-right bg-primary rounded-pill">${escapeHtml(message)}</p>
            </div>
        </div>`
    $("#messagesList").append(chatInfo);
}


function escapeHtml(unsafe) {
    return unsafe
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;")
};