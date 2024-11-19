$(document).ready(function () {

    var settings = {
        barWidth: 2,
        barHeight: 40
    };

    $("#barcode").barcode(
        $("#barcodeVal").val(), // Value barcode (dependent on the type of barcode)
        "code39", // type (string)
        settings
    );

    $("#btnPrint").click(function () {
        window.print();
    });

})