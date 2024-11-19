function checkValid() {
    let isValid = true;
    let message = "";
    if ($('#WriterName').val() == '') {
        isValid = false;
        message += "The name is required.\n\n";
    }

    if ($('#Phone').val() == '') {
        isValid = false;
        message += "The Phone number is required.\n\n";
    }

    if ($('#Email').val() == '') {
        isValid = false;
        message += "The Email is required.\n\n";
    }

    if ($('#Agency').val() == '') {
        isValid = false;
        message += "The agency is required.\n\n";
    }

    if ($('#FirstName').val() == '') {
        isValid = false;
        message += "The first name is required.\n\n";
    }

    if ($('#LastName').val() == '') {
        isValid = false;
        message += "The last name is required.\n\n";
    }

    if ($('#Address').val() == '') {
        isValid = false;
        message += "The address is required.\n\n";
    }

    if ($('#City').val() == '') {
        isValid = false;
        message += "The city is required.\n\n";
    }

    if ($('#Zip').val() == '') {
        isValid = false;
        message += "The zip is required.\n\n";
    }

    if (isValid) {
        $("#next").attr('class', 'btn btn-primary');
    } else {
        $("#next").attr('class', 'btn btn-secondary');
    }
    return { isValid, message };
}

$("#Zip").on('change', function () {
    $.ajax({
        url: "https://maps.googleapis.com/maps/api/geocode/json?address=" + $("#Zip").val() + "&key=",
        success: function (response) {
            if (response.status !== "ZERO_RESULTS") {
                var county = "";
                for (i = 0; i < response.results.length; i++) {
                    result = response.results[i];
                    adresses = result.address_components;
                    n = adresses.length;
                    for (j = 0; j < n; j++) {
                        type = adresses[j].types[0];
                        if (type == 'locality') county = (adresses[j].long_name);
                        if (type == 'administrative_area_level_2') {
                            county = (adresses[j].long_name);
                        }
                    }
                    if (county !== "Sonoma County" && county !== "Cloverdale") {
                        $("#Zip").val("");
                        $("#notSonoma").modal("show");
                    }
                }
            }
            else {
                $("#Zip").val("");
                $("#notSonoma").modal("show");
            }
        }
    });
});

$("#next").on('click', function () {
    const { isValid, message } = checkValid();
    if (isValid) {
        var AddressModel = {
            Address: $("#Address").val(),
            City: $("#City").val(),
            Zip: $("#Zip").val()
        };
        $.ajax({
            dataType: "json",
            url: "/SantaLetter/VerifyAddress",
            data: AddressModel,
            type: 'POST',
            success: function (data) {
                if (data.status === "suggested") {
                    $("#suggested_address").html(data.suggestedAddress.address);
                    $("#suggested_city").html(data.suggestedAddress.city);
                    $("#suggested_zip").html(data.suggestedAddress.zip);
                    $("#modalSuggestedAddress").modal("show");
                } else {
                    $('form#writerinfo').submit();
                }
            },
            error: function (jqXHR, exception) {
                window.location.href = "/Error";
            }
        });
    } else {
        swal({
            text: message,
            icon: 'warning'
        })
    }
});

$("#submit").on('click', function () {
    if ($("#use_suggested").is(":checked")) {
        $("#Address").val($("#suggested_address").html());
        $("#City").val($("#suggested_city").html());
        $("#Zip").val($("#suggested_zip").html());
    }
    $('form#writerinfo').submit();
});

$('#WriterName, #Phone, #Email, #FirstName, #LastName, #Address, #City, #Zip').on('keyup', function () {
    checkValid();
})

$('#Agency').on('change', function () {
    checkValid();
})