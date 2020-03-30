let addBtn = document.getElementById("addSubtaskButton");
let subtaskCreateDiv = document.getElementById("subtaskCreate");

addBtn.addEventListener("click", onClick);

function onClick() {
    // Creating elements
    let parentDiv = document.createElement("div");
    let inputDiv = document.createElement("div");
    let buttonDiv = document.createElement("div");

    let inputElement = document.createElement("input");
    let button = document.createElement("button");

    // Adding classes and etc.
    parentDiv.setAttribute("class", "row");
    inputDiv.setAttribute("class", "col-md-10");
    buttonDiv.setAttribute("class", "col-md-2");

    inputElement.setAttribute("placeholder", "Type short subtask description...");
    inputElement.setAttribute("class", "form-control");
    inputElement.setAttribute("type", "text");
    inputElement.setAttribute("data-val", "true");
    inputElement.setAttribute("data-val-length", "Subtask description shold be less than 100 symbols");
    inputElement.setAttribute("data-val-length-max", "100");
    inputElement.setAttribute("name", "Subtasks");
    button.setAttribute("class", "btn btn-primary");
    button.setAttribute("type", "button");
    button.textContent = "Add";
    button.addEventListener("click", onClick);

    // Appending childs

    inputDiv.appendChild(inputElement);
    buttonDiv.appendChild(button);

    parentDiv.appendChild(inputDiv);
    parentDiv.appendChild(buttonDiv);

    subtaskCreateDiv.appendChild(parentDiv);

    //Changing the pressed button
    this.removeAttribute("class");
    this.setAttribute("class", "btn btn-danger");
    this.textContent = "Delete!";
    this.removeEventListener("click", onClick);
    this.addEventListener("click", function () {
        this.parentNode.parentNode.remove();
    });
}