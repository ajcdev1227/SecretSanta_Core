$(document).ready(function () {
    $("#disclaimer").change(function () {
        if ($("#disclaimer").is(":checked")) {
            $("#submit").removeClass("d-none");
        }
        else if ($(this).is(":not(:checked)")) {
            $("#submit").addClass("d-none");
        }
    });
    $("#submit").click(function () {
        swal({
          title: $("#title").val(),
            text: $("#message").val(),
            type: "success"
        })
            .then(() => {
                $.ajax({
                    url: "/SantaLetter/SaveLetter",
                    success: function (data) {
                        window.location.href = "/Home/Index";
                    },
                    error: function (jqXHR, exception) {
                        window.location.href = "/Error";
                    }

                });
            });

    });

});