$(document).ready(function() {
    $("#add").click(function() {
        var id = $("#AgencyId").val();
        window.location.href = "/AgencyUser/AddAgencyPickUp/" + id;
    });
});