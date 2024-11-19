$(document).ready(function () {
    var id = $("#letterId").val();
    $("#add").click(function () {
        window.location.href = "/SantaLetter/AddFamilyMember/" + id;
    });
    $("#next").click(function () {
        window.location.href = "/SantaLetter/Review/" + id;
    });

    $("#skip").click(function () {
        window.location.href = "/SantaLetter/Review/" + id;
    });

});


