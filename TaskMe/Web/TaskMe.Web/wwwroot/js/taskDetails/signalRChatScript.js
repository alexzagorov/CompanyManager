let groupName = document.getElementById("taskId").innerHTML;
let userNames = document.getElementById("userNames").innerHTML;
let userPictureUrl = document.getElementById("userPictureUrl").innerHTML;

var connection =
    new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .build();

connection.on("NewMessage",
    function (object) {
        var today = new Date();
        var chatInfo = `<div class="card">
            <div class="card-body">
                <time class="card-text float-right" datetime="${today.toUTCString()}"></time>  
                <h6 class="card-subtitle mb-2 text-muted text-left">${object.user} <img class="rounded-circle border-dark" src="${object.pictureUrl}" width="25" height="25" /></h6>
                <h5 class="card-text float-left bg-secondary rounded-pill"><strong>${escapeHtml(object.text)}</strong></h5>
            </div>
        </div>`
        $("#messagesList").append(chatInfo);
        momentJs();
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
    var today = new Date();
    var chatInfo = `<div class="card">
            <div class="card-body">
                <time class="card-text" datetime="${today.toUTCString()}"></time>                        
                <h6 class="card-subtitle mb-2 text-muted text-right">${userNames} <img class="rounded-circle border-dark" src="${userPictureUrl}" width="25" height="25" /></h6>
                <h5 class="card-text float-right bg-primary rounded-pill"><strong>${escapeHtml(message)}</strong><h5>
            </div>
        </div>`
    $("#messagesList").append(chatInfo);
    momentJs();
}


function escapeHtml(unsafe) {
    return unsafe
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;")
};