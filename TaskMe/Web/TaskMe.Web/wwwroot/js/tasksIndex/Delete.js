let deleteBtns = document.getElementsByClassName("deleteButton");

let aHref = document.getElementById("checkArea").getAttribute("href");
let aspArea = aHref.split("/")[1];


for (let i = 0; i < deleteBtns.length; i++) {
    addEventListeners(deleteBtns[i])
}

function addEventListeners(item) {

    item.addEventListener("click", function () {

        let confirmBtn = document.getElementById("confirmButton");
        let taskId = item.getAttribute("name");

        confirmBtn.setAttribute("href", `/${aspArea}/Task/Delete?id=${taskId}`);
    });
};
