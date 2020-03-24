let confirmBtn = document.getElementById("confirmButton");
let fireBtn = document.getElementById("fireButton");

let userId = fireBtn.getAttribute("name");

confirmBtn.setAttribute("href", `/Manager/User/Delete?id=${userId}`);