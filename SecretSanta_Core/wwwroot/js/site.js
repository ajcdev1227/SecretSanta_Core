$(function () {
    $('.phone').inputmask("999-999-9999");
    $("#Zip").inputmask("99999");
    $("#tabs").tabs();
    $.widget.bridge('uitooltip', $.ui.tooltip);
    $(document).uitooltip();
});
