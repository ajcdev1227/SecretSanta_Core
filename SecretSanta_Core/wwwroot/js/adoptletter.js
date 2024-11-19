$(document).ready(function () {
    $('#radioBtn a').on('click',
        function () {
            var sel = $(this).data('title');
            var tog = $(this).data('toggle');
            $('#' + tog).prop('value', sel);

            $('a[data-toggle="' + tog + '"]').not('[data-title="' + sel + '"]').removeClass('active')
                .addClass('notActive');
            $('a[data-toggle="' + tog + '"][data-title="' + sel + '"]').removeClass('notActive').addClass('active');
            $("#information").css("display", "block");
        });

    $("#btnSubmit").click(function () {

        var isValid = $("form#info").valid();
        if (isValid) {

            if ($("#GiftMethod").val() === "Gift") {
                swal({
                  title: "Thank you!",
                        text: "Thank you for adopting this letter, check your email for confirmation",
                        type: "success"
                })
                    .then(() => {
                        $("form#info").submit();
                    });
            } else {
                $("form#info").submit();
            }
        }
    });

});