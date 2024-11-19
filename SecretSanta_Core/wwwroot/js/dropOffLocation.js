$(function () {
    $("#search").click(function () {
        window.location.href = "/LearnMore/DropOffZipCode?zipCode=" + $("#zipCode").val();
    });
});