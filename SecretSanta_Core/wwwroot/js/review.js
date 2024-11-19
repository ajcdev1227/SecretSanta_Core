$(document).ready(function() {
    var sizeTypes = ["Boys", "Girls", "Mens", "Womens"];
    var genders = ["M", "F", "NB"];
    var countFamilyMembers;
    var message = "";
    var familyMembers = new Array();


    $.ajax({
        dataType: "json",
        url: "/SantaLetter/GetFamilyMembers",
        data: { "letterId": $("#LetterId").val() },
        type: 'POST',
        success: function (data) {
            countFamilyMembers = data.familyMembers.length;
            if (data.familyMembers.length > 0) {

                for (var i = 0; i < data.familyMembers.length; i++) {
                    writeFamilyMember(data.familyMembers[i], i);
                }
            }
        },
        error: function(jqXHR, exception) {
            window.location.href = "/Error";
        }

    });


    $('#familyMembers').on('click', '.btnInfo', function () {
        $("#modalInfo").modal("show");
    });


    $("#next").click(function(e) {
        var isValid = ValidateInfo();
        if (isValid) {
            var AddressModel = {
                Address: $("#Address").val(),
                City: $("#City").val(),
                Zip: $("#Zip").val()

            };
            $.ajax({
                dataType: "json",
                url: "/SantaLetter/VerifyAddress",
                data:
                    AddressModel,
                type: 'POST',
                success: function(data) {
                    if (data.status === "suggested") {
                        $("#suggested_address").html(data.suggestedAddress.address);
                        $("#suggested_city").html(data.suggestedAddress.city);
                        $("#suggested_zip").html(data.suggestedAddress.zip);
                        $("#modalSuggestedAddress").modal("show");
                    } else {
                        SaveReview();
                    }
                },
                error: function(jqXHR, exception) {
                    window.location.href = "/Error";
                }

            });
        } else {
            $("#errorMessage").html(message);
            if (message !== "") {
                $("#modalWarning").modal("show");
            }
        }
    });

    $("#addFamilyMember").click(function() {
        addFamilyMember();
    });

    $("#submit").click(function () {
        if ($("#use_suggested").is(":checked")) {
            $("#Address").val($("#suggested_address").html());
            $("#City").val($("#suggested_city").html());
            $("#Zip").val($("#suggested_zip").html());
        }
        SaveReview();
    });


function writeFamilyMember(familyMember, index) {
    var html = "";
    html += "<input type='hidden' id='FamilyMemberId_" +
        index +
        "' class='member' value='" +
        familyMember.familyMemberId +
        "'/>";
    html += "<div class='form-group'>";
    html += "<div class='row'>";
    html += " <label for='Name_" + index + "' class='control-label col-md-4'>" + $("#Name").val() + "</label>";
    html += "<div class='col-md-8'>";
    html += "<input type='text' id='Name_" +
        index +
        "' class='form-control' value='" +
        familyMember.name +
        "'/>";
    html += "</div>";
    html += "</div>";
    html += "</div>";
    html += "<div class='form-group'>";
    html += "<div class='row'>";
    html += " <label for='Age_" + index + "' class='control-label col-md-4'>" + $("#Age").val() + "</label>";
    html += "<div class='col-md-8'>";
    html += "<input type='text' id='Age_" + index + "' class='form-control' value='" + familyMember.age + "'/>";
    html += "</div>";
    html += "</div>";
    html += "</div>";
    var genderOptions = "<option value=''>Select</option>";
    $.each(genders,
        function(i, value) {
            if (familyMember.gender == value) {
                genderOptions += "<option value='" + value + "' selected>" + value + "</option>";
            } else {
                genderOptions += "<option value='" + value + "'>" + value + "</option>";
            }
        });
    html += "<div class='form-group'>";
    html += "<div class='row'>";
    html += " <label for='Gender_" + index + "' class='control-label col-md-4'>" + $("#Gender").val() + "</label>";
    html += "<div class='col-md-8'>";
    html += "<select id='Gender_" + index + "' class='form-control'>";
    html += genderOptions;
    html += "</select>";
    html += "</div>";
    html += "</div>";
    html += "</div>";
    var clothingOptions = "<option value=''>Select</option>";
    $.each(sizeTypes,
        function(i, value) {
            if (familyMember.warmClothingType == value) {
                clothingOptions += "<option value='" + value + "' selected>" + value + "</option>";
            } else {
                clothingOptions += "<option value='" + value + "'>" + value + "</option>";
            }
        });
    html += "<div class='form-group'>";
    html += "<div class='row'>";
    html += "<div class='col-md-4' style ='text-align: right'><label class='control-label'>" +
        $("#WarmClothingSize").val() +
        "</label></div>";
    html += "<div class='col-md-8 info-button'>";
    html += "<a class='btnInfo' class='info btn' title='" + $("#WarmClothingSizeInfo").val() + "'>";
    html += "<i class='fa fa-info-circle'></i>";
    html += "</a>";
    html += "</div>";
    html += "</div>";
    html += "</div>";
    html += "<div class='form-group'>";
    html += "<div class='row'>";
    html += "<div class='col-md-4'><label for='WarmClothingSize' class='control-label'>" + $("#Size").val() + "</label></div>";
    html += "<div class='col-md-4'>";
    html += "<input type='text' id='WarmClothingSize_" +
        index +
        "' class='form-control' value='" +
        familyMember.warmClothingSize +
        "'/>";
    html += "</div>";
    html += "<div class='col-md-4'>";
    html += "<select id='WarmClothingType_" + index + "' class='form-control'>";
    html += clothingOptions;
    html += "</select>";
    html += "</div>";
    html += "</div>";
    html += "</div>";
    var shoeOptions = "<option value=''>Select</option>";
    $.each(sizeTypes,
        function(i, value) {
            if (familyMember.shoeSizeType == value) {
                shoeOptions += "<option value='" + value + "' selected>" + value + "</option>";
            } else {
                shoeOptions += "<option value='" + value + "'>" + value + "</option>";
            }
        });
    html += "<div class='form-group'>";
    html += "<div class='row'>";
    html += " <label for='ShoeSize_" + index + "' class='control-label col-md-4'>" + $("#ShoeSize").val() + "</label>";
    html += "<div class='col-md-4'>";
    html += "<input type='text' id='ShoeSize_" +
        index +
        "' class='form-control' value='" +
        familyMember.shoeSize +
        "'/>";
    html += "</div>";
    html += "<div class='col-md-4'>";
    html += "<select id='ShoeSizeType_" + index + "' class='form-control'>";
    html += shoeOptions;
    html += "</select>";
    html += "</div>";
    html += "</div>";
    html += "</div>";
    html += "<div class='form-group'>";
    html += "<div class='row'>";
    html += " <label for='Likes_" + index + "' class='control-label col-md-4'>" + $("#Likes").val() + "</label>";
    html += "<div class='col-md-8'>";
    html += "<textarea id='Likes_" +
        index +
        "' class='form-control' rows='10'>" +
        familyMember.likes +
        "</textarea>";
    html += "</div>";
    html += "</div>";
    html += "</div>";
    html += "<div class='form-group'>";
    html += "<div class='row'>";
    html += " <label for='OtherRequests_" +
        index +
        "' class='control-label col-md-4'>" +
        $("#OtherRequests").val() +
        "</label>";
    html += "<div class='col-md-8'>";
    var otherRequests = familyMember.otherRequests != null ? familyMember.otherRequests : "";
    html += "<textarea id='OtherRequests_" +
        index +
        "' class='form-control' rows='10'>" +
        otherRequests +
        "</textarea>";
    html += "</div>";
    html += "</div>";
    html += "</div>";

    $("#familyMembers").append(html);
}

    function addFamilyMember() {
        var html = "";
        html += "<input type='hidden' id='FamilyMemberId_" + countFamilyMembers + "' class='member' value='0'/>";
        html += "<div class='form-group'>";
        html += "<div class='row'>";
        html += " <label for='Name_" +
            countFamilyMembers +
            "' class='control-label col-md-4'>" +
            $("#Name").val() +
            "</label>";
        html += "<div class='col-md-8'>";
        html += "<input type='text' id='Name_" +
            countFamilyMembers +
            "' class='form-control', maxlength='150'/>";
        html += "</div>";
        html += "</div>";
        html += "</div>";
        html += "<div class='form-group'>";
        html += "<div class='row'>";
        html += " <label for='Age_" +
            countFamilyMembers +
            "' class='control-label col-md-4'>" +
            $("#Age").val() +
            "</label>";
        html += "<div class='col-md-8'>";
        html += "<input type='text' id='Age_" + countFamilyMembers + "' class='form-control' />";
        html += "</div>";
        html += "</div>";
        html += "</div>";
        var genderOptions = "<option value=''>Select</option>";
        $.each(genders,
            function(i, value) {
                genderOptions += "<option value='" + value + "'>" + value + "</option>";
            });
        html += "<div class='form-group'>";
        html += "<div class='row'>";
        html += " <label for='Gender_" +
            countFamilyMembers +
            "' class='control-label col-md-4'>" +
            $("#Gender").val() +
            "</label>";
        html += "<div class='col-md-8'>";
        html += "<select id='Gender_" + countFamilyMembers + "' class='form-control'>";
        html += genderOptions;
        html += "</select>";
        html += "</div>";
        html += "</div>";
        html += "</div>";
        var clothingOptions = "<option value=''>Select</option>";
        $.each(sizeTypes,
            function(i, value) {

                clothingOptions += "<option value='" + value + "'>" + value + "</option>";
            });
        html += "<div class='row'>";
        html += "<div class='col-md-4' style ='text-align: right'><label class='control-label'>" +
            $("#WarmClothingSize").val() +
            "</label></div>";
        html += "<div class='col-md-8 info-button'>";
        html += "<a class='btnInfo' class='info btn' title='" + $("#WarmClothingSizeInfo").val() + "'>";
        html += "<i class='fa fa-info-circle'></i>";
        html += "</a>";
        html += "</div>";
        html += "</div>";
        html += "<div class='row'>";
        html +=
            "<div class='col-md-4'><label for='WarmClothingSize' class='control-label'>" + $("#Size").val() + "</label></div>";
        html += "<div class='col-md-4'>";
        html += "<input type='text' id='WarmClothingSize_" +
            countFamilyMembers +
            "' class='form-control', maxlength='25'/>";
        html += "</div>";
        html += "<div class='col-md-4'>";
        html += "<select id='WarmClothingType_" + countFamilyMembers + "' class='form-control'>";
        html += clothingOptions;
        html += "</select>";
        html += "</div>";
        html += "</div>";
        var shoeOptions = "<option value=''>Select</option>";
        $.each(sizeTypes,
            function(i, value) {
                shoeOptions += "<option value='" + value + "'>" + value + "</option>";
            });
        html += "<div class='form-group'>";
        html += "<div class='row'>";
        html += " <label for='ShoeSize_" +
            countFamilyMembers +
            "' class='control-label col-md-4'>" +
            $("#ShoeSize").val() +
            "</label>";
        html += "<div class='col-md-4'>";
        html += "<input type='text' id='ShoeSize_" +
            countFamilyMembers +
            "' class='form-control', maxlength='25'/>";
        html += "</div>";
        html += "<div class='col-md-4'>";
        html += "<select id='ShoeSizeType_" + countFamilyMembers + "' class='form-control'>";
        html += shoeOptions;
        html += "</select>";
        html += "</div>";
        html += "</div>";
        html += "</div>";
        html += "<div class='form-group'>";
        html += "<div class='row'>";
        html += " <label for='Likes_" +
            countFamilyMembers +
            "' class='control-label col-md-4'>" +
            $("#Likes").val() +
            "</label>";
        html += "<div class='col-md-8'>";
        html += "<textarea id='Likes_" +
            countFamilyMembers +
            "' class='form-control' rows='10'></textarea>";
        html += "</div>";
        html += "</div>";
        html += "</div>";
        html += "<div class='form-group'>";
        html += "<div class='row'>";
        html += " <label for='OtherRequests_" +
            countFamilyMembers +
            "' class='control-label col-md-4'>" +
            $("#OtherRequests").val() +
            "</label>";
        html += "<div class='col-md-8'>";
        html += "<textarea id='OtherRequests_" +
            countFamilyMembers +
            "' class='form-control' rows='10'></textarea>";
        html += "</div>";
        html += "</div>";
        html += "</div>";


        $("#familyMembers").append(html);
        $([document.documentElement, document.body]).animate({
            scrollTop: $("#Name_" + countFamilyMembers).offset().top
            },
            2000);
        $("#Name_" + countFamilyMembers).focus();
        countFamilyMembers++;

    }

    function ValidateInfo() {
        $("#errorMessage").empty();
        message = "";
        familyMembers = new Array();
``
        var valid = $("#review").valid();

        var sum = parseInt($("#NumChildrenUnder19").val()) +
            parseInt($("#NumChildrenOver19").val()) +
            parseInt($("#NumParents").val()) +
            parseInt($("#NumGrandparents").val()) +
            parseInt($("#NumOtherFamily").val()) +
            parseInt($("#NumFriends").val());
        if (sum === 0) {
            valid = false;
            message += $("#FamilyCountWarning").val() + + "<br/>";
        }
      
        $(".member").each(function() {
            var id = $(this).attr("id");
            var nameId = id.replace("FamilyMemberId", "Name");
            var ageId = id.replace("FamilyMemberId", "Age");
            var genderId = id.replace("FamilyMemberId", "Gender");
            var clothingId = id.replace("FamilyMemberId", "WarmClothingSize");
            var clothingTypeId = id.replace("FamilyMemberId", "WarmClothingType");
            var shoeId = id.replace("FamilyMemberId", "ShoeSize");
            var shoeTypeId = id.replace("FamilyMemberId", "ShoeSizeType");
            var likesId = id.replace("FamilyMemberId", "Likes");
            var requestsId = id.replace("FamilyMemberId", "OtherRequests");
            var memberComplete =
                $("#" + nameId).val() != "" &&
                    $("#" + ageId).val() != "" &&
                    $("#" + genderId).val() != "" &&
                    $("#" + clothingId).val() != "" &&
                    $("#" + clothingTypeId).val() != "" &&
                    $("#" + shoeId).val() != "" &&
                    $("#" + shoeTypeId).val() != "" &&
                    $("#" + likesId).val() != "";
            var memberEmpty =
                $("#" + nameId).val() == "" &&
                    $("#" + ageId).val() == "" &&
                    $("#" + genderId).val() == "" &&
                    $("#" + clothingId).val() == "" &&
                    $("#" + clothingTypeId).val() == "" &&
                    $("#" + shoeId).val() == "" &&
                    $("#" + shoeTypeId).val() == "" &&
                    $("#" + likesId).val() == "" &&
                    $("#" + requestsId).val() == "";

            if (!memberComplete && !memberEmpty) {
                valid = false;
                message += $("#FamilyMemberWarning").val() + "<br/>";
            }
            if (memberComplete) {
                var familyMember = {
                    FamilyMemberId: $(this).val(),
                    Name: $("#" + nameId).val(),
                    Age: $("#" + ageId).val(),
                    Gender: $("#" + genderId).val(),
                    WarmClothingSize: $("#" + clothingId).val(),
                    WarmClothingType: $("#" + clothingTypeId).val(),
                    ShoeSize: $("#" + shoeId).val(),
                    ShoeSizeType: $("#" + shoeTypeId).val(),
                    Likes: $("#" + likesId).val(),
                    OtherRequests: $("#" + requestsId).val()
                };
                familyMembers.push(familyMember);
            }

        });
        return valid;
    }

    function SaveReview() {
        var ReviewModel = {
            LetterId: $("#LetterId").val(),
            Language: $("#Language").val(),
            Address: $("#Address").val(),
            City: $("#City").val(),
            Zip: $("#Zip").val(),
            Phone: $("#Phone").val(),
            Email: $("#Email").val(),
            NumChildrenUnder19: $("#NumChildrenUnder19").val(),
            NumChildrenOver19: $("#NumChildrenOver19").val(),
            NumParents: $("#NumParents").val(),
            NumGrandparents: $("#NumGrandparents").val(),
            NumOtherFamily: $("#NumOtherFamily").val(),
            Letter: $("#Letter").val(),
            FirstName: $("#FirstName").val(),
            LastName: $("#LastName").val(),
            WriterName: $("#WriterName").val(),
            NumFriends: $("#NumFriends").val() != null ? $("#NumFriends").val() : 0,
            FamilyMembers: familyMembers
        };
        $.ajax({
            dataType: "json",
            url: "/SantaLetter/SaveReview",
            data:
                ReviewModel,
            type: 'POST',
            success: function (data) {
                window.location.href = "/SantaLetter/Disclaimer";
            },
            error: function (jqXHR, exception) {
                window.location.href = "/Error";
            }

        });
    }

});




