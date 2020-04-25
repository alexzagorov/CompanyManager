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
                if (data != null && data.length > 0) {
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
                } else if (data == null) {
                    $("#employees").append(`
                        <tr>
                            <td class="text-danger">There are no emoloyees at this moment!</td>
                        </tr>`);                        
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