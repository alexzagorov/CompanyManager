let companyImgElement = document.getElementById("companyPicture");
let companyBtn = document.getElementById("companyButton");
let supervisorBtn = document.getElementById("supervisorButton");

companyBtn.addEventListener("click", function () {
    companyImgElement.style.display = "block";
});

supervisorBtn.addEventListener("click", function () {
    companyImgElement.style.display = "none";
});