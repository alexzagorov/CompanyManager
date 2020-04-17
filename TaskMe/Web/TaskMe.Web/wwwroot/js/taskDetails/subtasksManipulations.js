let takeBtns = document.getElementsByClassName("takeButton");

for (var i = 0; i < takeBtns.length; i++) {

    takeBtns[i].addEventListener("click", function () {
        let currentBtn = this;
        let requestInfo = currentBtn.getAttribute("name");
        let subtaskId = requestInfo.split(",")[0];
        let userId = requestInfo.split(",")[1];
        let aspArea = requestInfo.split(",")[2]
        if (aspArea != undefined) {
            aspArea = `&aspArea=${aspArea}`;
        } else {
            aspArea = "";
        }

        $.ajax({
            type: 'GET',
            url: '/Subtask/TakeSubtask',
            data: { "subtaskId": subtaskId, "userId": userId },
            dataType: 'json',
            success: function (data) {
                if (data != null) {
                    let ElementToAddTo = currentBtn.parentNode.parentNode;
                    let subtaskDescElement = ElementToAddTo.getElementsByClassName("subtaskDesc")[0];
                    let subtaskDescHtml = subtaskDescElement.outerHTML;
                    ElementToAddTo.innerHTML = `
                     ${subtaskDescHtml}
                    <p class="mb-1">Taken (By: ${data})<i class="fa fa-check" style="color: forestgreen"></i></p>
                    <p class="mb-1">Ready <i class="fa fa-times" style="color: red"></i> <a class="btn btn-success" href="/Subtask/FinishSubtask?subtaskId=${subtaskId}${aspArea}">Mark as finished</a> </p>`;
                }
            },           
            error: function () {
                alert("Error while executing the process!");
            }
        });
    });
}