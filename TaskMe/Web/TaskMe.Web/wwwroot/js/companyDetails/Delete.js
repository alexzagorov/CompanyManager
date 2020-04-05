let fireBtns = document.getElementsByClassName("fireButton");


for (let i = 0; i < fireBtns.length; i++) {
    addEventListeners(fireBtns[i])
}

function addEventListeners(item) {
    item.addEventListener("click", function () {
        let confirmBtn = document.getElementById("confirmButton");

        let userId = item.getAttribute("name");

        confirmBtn.setAttribute("href", `/Manager/User/Delete?id=${userId}`);
    });
};
