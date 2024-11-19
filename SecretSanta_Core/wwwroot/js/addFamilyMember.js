$(document).ready(function () {

    $("#btnInfo").click(function () {
        $("#modalInfo").modal("show");
    });

});

var form = $("#addfamily");
$('#Name, #Age, #WarmClothingSize, #ShoeSize, #Likes, #OtherRequests').on('keyup', function () {
    if (form.valid()) {
        $("#add").prop('disabled', false);
    } else {
        $("#add").prop('disabled', true);
    }
})

$('#Gender, #WarmClothingType, #ShoeSizeType').on('change', function () {
    if (form.valid()) {
        $("#add").prop('disabled', false);
    } else {
        $("#add").prop('disabled', true);
    }
})