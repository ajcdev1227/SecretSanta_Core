$(document).ready(function () {
    var settings = {
        barWidth: 2,
        barHeight: 60
    };

    $("#enter").click(function () {

        if ($("#labelNo").val() == "") {
            swal("Please enter Label Number or Recipient Number!");
            return;
        }

        $.ajax({
            dataType: "json",
            url: "/MyHeart/GetLabel",
            data: { "heartNumber": $("#labelNo").val() },
            type: 'POST',
            success: function (data) {
                if (data.status == "success") {
                    $("#name").text(data.heart.name);
                    $("#lNo").text(data.heart.labelNum);
                    $("#agencyCode").text(data.heart.agencyCode);
                    $("#barcodeVal").val(data.heart.labelNum);
                    $("#barcode").barcode(
                        $("#barcodeVal").val(),
                        "code39",
                        settings
                    );
                    $("#recNo").text(data.heart.recipientNumber);
                    $("#RecipientNum").val(data.heart.recipientNumber);
                    $("#yr").text(data.year);
                    $("#info").css("display", "block");

                } else {
                    swal(data.message);
                }
            },
            error: function () {
                window.location.href = "/Error";
            }
        });
    });

    $("#btnSubmit")
        .click(function (e) {
            e.preventDefault();
            if ($("#agree").is(":checked")) {
                $('form#register').submit();
            } else {
                swal("Please check that you agree to the terms");
            }
        });
});