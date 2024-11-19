$(function () {
    $("#tabs").tabs();

    $("#btnUpload").click(function () {
        var formData = new FormData();
        var t = $('#image')[0].files[0];
        formData.append('file', $('#image')[0].files[0]);
        $.ajax({
            url: "/ApplicationSettings/SaveImageFiles",
            type: 'POST',
            processData: false,
            contentType: false,
            data: formData
        })
            .done(function (data) {
                GetData();
                $("#tabs").tabs("option", "active", 1);
            })
            .fail(function (jqXHR, textStatus, errorThrown) { window.location.href = "/Error"; });
    });

    $("#gallery")
        .on("click",
            ".chk",
            function () {
                $('.chk').not(this).prop('checked', false);

            });
    $("#set")
        .click(function () {
            var imageName;
            $('.chk').each(function () {
                if ($(this).prop('checked')) {
                    imageName = $(this).attr("id");
                }
            });
            $.ajax({
                dataType: "json",
                type: 'POST',
                data: {
                    name: imageName
                },
                url: "/ApplicationSettings/SetImage",
                async: false,
                success: function (data) {
                    window.location.href = "/ApplicationSettings/Index";
                },
                error: function (jqXHR, exception) {
                    alert("Sorry, there has been an error");
                }
            });
        });
});

function GetData() {
    $.ajax({
        dataType: "json",
        url: "/ApplicationSettings/GetImages",
        success: function (data) {
            WriteData(data);
        },
        error: function (jqXHR, exception) {
            alert("Sorry, there has been an error");
        }
    });
}

function WriteData(data) {
    $("#gallery").empty();
    var strHtml = "";
    for (var i = 0; i < data.model.length; i += 6) {
        strHtml += "<div class='row'>";
        for (var j = i; j <= Math.min(5 + i, data.model.length - 1); j++) {
            strHtml += "<div class='col-md-2'>";
            strHtml += " <div class='member'>";
            strHtml += "<div class='gallery-img' style='background-image: url(\"" +
                data.model[j].blobImageUri +
                "\");'></div>";
            if (i == 0 && j == 0) {
                strHtml += "<div>";
                strHtml += "<input type='checkbox' id='" +
                    data.model[j].blobImageName +
                    "' class='chk' checked='checked' />";
                strHtml += "</div>";
            } else {
                strHtml += "<div>";
                strHtml += "<input type='checkbox' id='" +
                    data.model[j].blobImageName +
                    "' class='chk'/>";
                strHtml += "</div>";
            }

            strHtml += "</div>";
            strHtml += "</div>";
        }
        strHtml += "</div>";
    }
    $("#gallery").append(strHtml);
}