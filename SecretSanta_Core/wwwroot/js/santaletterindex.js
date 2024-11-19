$(document).ready(function () {

    $("#disclaimer").change(function () {
        if ($("#disclaimer").is(":checked")) {
            $("#next").css("display", "block");
        }
        else if ($(this).is(":not(:checked)")) {
            $("#next").css("display", "none");
        }
    });
    $("#next").click(function () {
        window.location.href = "/SantaLetter/WriterInfo";
    });

});