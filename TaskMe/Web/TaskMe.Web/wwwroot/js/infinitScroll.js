    var pageSize = 10;
    var pageIndex = 0;

    $(document).ready(function () {
        GetData();

        $(".scrollable").scroll(function () {
            if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
                GetData();
            }
        });
    });

    function GetData() {
        $.ajax({
            type: 'GET',
            url: 'LoadEmployees',
            data: { "pageindex": pageIndex, "pagesize": pageSize },
            dataType: 'json',
            success: function (data) {
                if (data != null) {
                    for (var i = 0; i < data.length; i++) {
                        $("#employees").append(` 
<tr> 
<td>
<img class=\"rounded-circle border-dark\" src=\"${data[i].pictureUrl}" width=\"20\" height=\"20\" /> <strong>${data[i].firstName} ${data[i].lastName}</strong>
</td>
<td>
<strong>Email: ${data[i].email} </strong>
</td>
<td>
<strong>Form: ${data[i].createdOnShort} </strong>
</td>
</tr>`);
                    }
                    pageIndex++;
                }
            },
            beforeSend: function () {
                $("#progress").show();
            },
            complete: function () {
                $("#progress").hide();
            },
            error: function () {
                alert("Error while retrieving data!");
            }
        });
};