$(function() {
    WriteThankYous();

    $("#table").on('click',
        '.viewImage',
        function () {
            $("#thankYouImage").removeAttr("src").addClass("hidden");
            var col = $(this).parent();
            var hidden = col.find('.id');
            var id = hidden.val();
            var recipientName = col.find(".name").val();
            $.ajax({
                dataType: "json",
                url: "/AdminThankYou/GetImageName",
                data: { "donorThankYouId": id },
                type: 'POST',
                success: function (data) {
                    var baseUrl = "https://secretsantastore.blob.core.windows.net/thankyous/";
                    var imageUrl = baseUrl + data.imageName;
                    $("#thankYouImage").attr("src", imageUrl).removeClass("hidden");
                    $("#imageName").text(recipientName);
                    $("#image").modal("show");
                },
                error: function (jqXHR, exception) {
                    window.location.href = "/Error";
                }

            });
            
        });
    $("#table").on('click',
        '.viewMessage',
        function () {
            var col = $(this).parent();
            var hidden = col.find('.id');
            var id = hidden.val();
            var recipientName = col.find(".name").val();
            $.ajax({
                dataType: "json",
                url: "/AdminThankYou/GetMessage",
                data: { "donorThankYouId": id },
                type: 'POST',
                success: function (data) {
                    $("#thankYouMessage").text(data.message);
                    $("#messageName").text(recipientName);
                    $("#thankYouId").val(id);
                    $("#message").modal("show");
                },
                error: function (jqXHR, exception) {
                    window.location.href = "/Error";
                }

            });

        });

    $("#save").click(function () {
        $.ajax({
            dataType: "json",
            url: "/AdminThankYou/SaveMessage",
            data: { "donorThankYouId": $("#thankYouId").val(), "message": $("#thankYouMessage").text() },
            type: 'POST',
            success: function (data) {
                $("#message").modal("hide");
            },
            error: function (jqXHR, exception) {
                window.location.href = "/Error";
            }

        });

    });

    $("#table").on('change',
        '.approve',
        function () {
            var row = $(this).parent().parent();
            var hidden = row.find('.id');
            var id = hidden.val();
            var checkbox = row.find('.approve');
            var check = $(checkbox).is(':checked');
            $.ajax({
                dataType: "json",
                url: "/AdminThankYou/ChangeApprove",
                data: { "donorThankYouId":id, "check": check },
                type: 'POST',
                success: function (data) {
                    WriteThankYous();
                },
                error: function (jqXHR, exception) {
                    window.location.href = "/Error";
                }

            });
        });
});

function WriteThankYous() {
    $.ajax({
        url: '/AdminThankYou/GetThankYous',
        success: function (data) {
            WriteTable(data);
        },
        error: function (jqXHR, exception) {
            window.location.href = "/Error";
        }
    });
}

function WriteTable(data) {

        $("#table").empty();
        var stringHtml = "<table class='table'>";
        stringHtml += "<tr><th width='25%'>Recipient Name</th><th width='25%'>Thank You Date</th><th width='25%'>View / Edit</th><th width='25%'>Approve</th></td>";
        if (data.result.length > 0) {
            for (var i = 0; i < data.result.length; i++) {
                stringHtml += "<tr>";
                stringHtml += "<td>" + data.result[i].recipientName + "</td>";
                stringHtml += "<td>" + data.result[i].thankYouDate + "</td>";
                stringHtml += "<td>";
                if (data.result[i].message !== "") {
                    stringHtml += "<a href='#' class='viewMessage'>View Message</a>";
                }
                if (data.result[i].image!== "") {
                    stringHtml += "<a href='#' class='viewImage'>View Image</a>";
                }
                stringHtml += "<input type='hidden' class='id' value='" + data.result[i].donorThankYouId + "'>";
                stringHtml += "<input type='hidden' class='name' value='" + data.result[i].recipientName + "'></td>";
                stringHtml += "<td>";
                if (data.result[i].approved === true) {
                    stringHtml += "<input class='approve' type='checkbox' checked='checked'/>";
                } else {
                    stringHtml += "<input class='approve' type='checkbox'/>";
                }
                stringHtml += "</td>";
                stringHtml += "</tr>";
            }
        }
        stringHtml += "</table>";
        $("#table").append(stringHtml);
}