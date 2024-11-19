
function checkValid() {
    if ($("form#info").valid() && $("#GiftMethod").val() != "") {
        $('#btnSubmit').prop('disabled', false);
    } else {
        $('#btnSubmit').prop('disabled', true);
    }
}

$('#radioBtn a').on('click',
    function () {
        var sel = $(this).data('title');
        var tog = $(this).data('toggle');
        $('#' + tog).prop('value', sel);
        checkValid();
        $('a[data-toggle="' + tog + '"]').not('[data-title="' + sel + '"]')
            .removeClass('active')
            .addClass('notActive');

        $('a[data-toggle="' + tog + '"][data-title="' + sel + '"]')
            .removeClass('notActive')
            .addClass('active');

        $.ajax({
            dataType: "json",
            url: "/AdoptHeart/CheckDonor",
            data: { "recipientNum": $("#recipientNumber").val() },
            type: 'POST',
            success: function (data) {
                if (data.count > 0) {
                    $("#adopted").css("display", "block");
                } else {
                    $("#information").css("display", "block");
                }
            },
            error: function (jqXHR, exception) {
                window.location.href = "/Error";
            }

        });
    });

$("#back").click(function () {
    window.location.href = "/AdoptHeart/AdoptFromHome?gender=" + $("#gender").val() + "&ageGroup=" + $("#age").val() + "&giftType=" + $("#giftType").val();
});

$('#donorEmail, #confirm_donorEmail').on('change keyup paste input', function () {
    checkValid();
})

$("#btnSubmit").click(function (event) {
    event.preventDefault();
    if ($("#GiftMethod").val() == "") {
        swal({
            title: "",
            text: 'Please select "Fund this heart" or "Print this Heart" first',
            type: "warning"
        });
        return;
    }
    var isValid = $("form#info").valid();
    if (isValid) {
        if ($("#GiftMethod").val() == "Gift") {
            swal({
                title: "Thank you!",
                text: "Thank you for adopting this heart, check your email for your heart registration",
                type: "success"
            }).then(function () {
                $("form#info").submit();
            });
        } else {
            $("form#info").submit();
        }

    }
});
