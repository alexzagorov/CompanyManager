var pageIndex = 1;
var taskId = document.getElementById("taskId").innerHTML;

$(document).ready(function () {
    GetData();

    $(".scrollable").scroll(function () {
        if ($(this).scrollTop() == 0) {
            GetData();
        }
    });
});

function GetData() {
    $.ajax({
        type: 'GET',
        url: '/Message/LoadMessages',
        data: { "pageindex": pageIndex, "taskid": taskId },
        dataType: 'json',
        success: function (data) {
            if (data != null) {
                for (var i = 0; i < data.length; i++) {

                    if (data[i].writerPictureUrl === userPictureUrl) {
                        $(".scrollable").prepend(`
                        <div class="card">
                            <div class="card-body">
                                <time class="card-text" datetime="${data[i].createdOnString}"></time>      
                                <h6 class="card-subtitle mb-2 text-muted text-right">${data[i].writerNames} <img class="rounded-circle border-dark" src="${data[i].writerPictureUrl}" width="25" height="25" /></h6>
                                <h5 class="card-text float-right bg-primary rounded-pill"><strong>${escapeHtml(data[i].text)}</strong></h5>
                            </div>
                        </div>`);
                    }else {
                        $(".scrollable").prepend(`
                        <div class="card">
                            <div class="card-body">
                                <time class="card-text float-right" datetime="${data[i].createdOnString}"></time>      
                                <h6 class="card-subtitle mb-2 text-muted text-left">${data[i].writerNames} <img class="rounded-circle border-dark" src="${data[i].writerPictureUrl}" width="25" height="25" /></h6>
                                <h5 class="card-text float-left bg-secondary rounded-pill"><strong>${escapeHtml(data[i].text)}</strong></h5>
                            </div>
                        </div>`);
                    }                    
                }
                pageIndex++;
            }
        },
        beforeSend: function () {
            $("#progress").show();
        },
        complete: function () {
            $("#progress").hide();
            if (pageIndex - 1 === 1) {
                $(".scrollable").animate({ scrollTop: $('.scrollable').prop("scrollHeight") }, 1000);;
            }
            momentJs();
        },
        error: function () {
            alert("Error while retrieving data!");
        }
    });
};

function escapeHtml(unsafe) {
    return unsafe
        .replace(/&/g, "&amp;")
        .replace(/</g, "&lt;")
        .replace(/>/g, "&gt;")
        .replace(/"/g, "&quot;")
        .replace(/'/g, "&#039;")
};