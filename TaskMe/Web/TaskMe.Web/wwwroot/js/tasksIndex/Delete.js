let deleteBtns = document.getElementsByClassName("deleteButton");


for (let i = 0; i < deleteBtns.length; i++) {
    addEventListeners(deleteBtns[i])
}

function addEventListeners(item) {

    item.addEventListener("click", function () {

        let confirmBtn = document.getElementById("confirmButton");
        let taskId = item.getAttribute("name");

        confirmBtn.setAttribute("href", `/Manager/Task/Delete?id=${taskId}`);
    });
};
