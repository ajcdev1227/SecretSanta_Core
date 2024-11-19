var details = new Array();
$(document).ready(function () {
    $.ajax({
        url: "/GiftRecipient/GetGiftDetails",
        success: function (data) {
            for (var i = 0; i < data.result.length; i++) {
                var giftDetail = {
                    "giftDetailId": data.result[i].giftDetailId, "giftIdeaDescription": data.result[i].giftIdeaDescription, "giftDetailText": data.result[i].giftDetailText,
                    "lblGiftDetail1": data.result[i].lblGiftDetail1, "lblGiftDetail2": data.result[i].lblGiftDetail2
                };
                details.push(giftDetail);
            }
        },
        error: function (jqXHR, exception) {
            alert(jqXHR.responseText);
        }


    });
});

$("#GiftType").on('change', function () {
    $("#showSize").css("display", "none");

    var id = $(this).val();
    if (id != "") {
        GetGiftDetails(id);
    }
    if ($("#GiftType option:selected").text() == "Gift Card" ||
        $("#GiftType option:selected").text() == "Bike" ||
        $("#GiftType option:selected").text() == "Shoes") {
        $("#GiftWish").val($("#GiftType option:selected").text() + "; ");
    } else {
        $("#GiftWish").val("");
    }
    GetGiftWishString();
    checkValid();
});

$("#AltGiftType").on('change', function () {
    $("#altShowSize").css("display", "none");
    var id = $(this).val();
    if (id != "") {
        GetAltGiftDetails(id);
    }
    if ($("#AltGiftType option:selected").text() == "Gift Card" ||
        $("#AltGiftType option:selected").text() == "Bike" ||
        $("#AltGiftType option:selected").text() == "Shoes") {
        $("#AltGiftWish").val($("#AltGiftType option:selected").text() + "; ");
    } else {
        $("#AltGiftWish").val("");
    }
    GetAltGiftWishString();
    checkValid();
});

$("#btnNameInfo").on('click', function () {
    $("#modalNameInfo").modal("show");
});

$("#btnRecipientInfo").on('click', function () {
    $("#modalRecipientInfo").modal("show");
});

$(".info-wish").on('click', function () {
    $("#modalWishInfo").modal("show");
});

$("#Name, #Age").on('keyup', function () {
    GetNameAgeGenderString();
    checkValid();
});

$("#Gender, #AgeType").on('change', function () {
    GetNameAgeGenderString();
    checkValid();
});

$("#RecipientInfo").on('keyup', function () {
    $("#heart_recipient_info").html($("#RecipientInfo").val());
    checkValid();
});

$("#GiftWish, #GiftDetail1, #GiftDetail2").on('keyup', function () {
    GetGiftWishString();
    checkValid();
});

$("#AltGiftWish, #AltGiftDetail1, #AltGiftDetail2").on('keyup', function () {
    GetAltGiftWishString();
    checkValid();
});


function checkValid() {
    var message = "";
    var isValid = true;

    if ($("#Name").val() == "") {
        isValid = false;
        message += "Name is required<br/>";
    }

    if ($("#Age").val() == "") {
        isValid = false;
        message += "Age is required<br/>";
    }
    if ($("#AgeType").val() == "") {
        isValid = false;
        message += "Age type is required<br/>";
    }
    if ($("#Gender").val() == "") {
        isValid = false;
        message += "Gender is required<br/>";
    }

    if ($("#RecipientInfo").val() == "") {
        isValid = false;
        message += "Info is required<br/>";
    }

    if ($("#GiftType").val() == "") {
        isValid = false;
        message += "Gift type is required<br/>";
    }

    if ($("#GiftWish").val() == "") {
        isValid = false;
        message += "Gift wish is required<br/>";
    }

    if ($("#AltGiftType").val() == "") {
        isValid = false;
        message += "Second gift type is required<br/>";
    }

    if ($("#AltGiftWish").val() == "") {
        isValid = false;
        message += "Second gift wish is required<br/>";
    }

    if ($("#locationNecessary").val() == "True" && $("#Location").val() == "") {
        isValid = false;
        message += "Location is required<br/>";
    }

    var id = $("#GiftType").val();
    if (id !== "") {
        var giftDetail = details.find(d => d.giftDetailId == id);
        if (giftDetail != undefined && giftDetail.lblGiftDetail1 != null && $("#GiftDetail1").val() == "") {
            isValid = false;
            message += "Detail is required<br/>";

        }
        if (giftDetail != undefined && giftDetail.lblGiftDetail2 != null && $("#GiftDetail2").val() == "") {
            isValid = false;
            message += "Detail is required<br/>";

        }
    }

    var ida = $("#AltGiftType").val();
    if (ida !== "") {
        var giftDetail = details.find(d => d.giftDetailId == ida);
        if (giftDetail != undefined && giftDetail.lblGiftDetail1 != null && $("#AltGiftDetail1").val() == "") {
            isValid = false;
            message += "Detail is required<br/>";

        }
        if (giftDetail != undefined && giftDetail.lblGiftDetail2 != null && $("#AltGiftDetail2").val() == "") {
            isValid = false;
            message += "Detail is required<br/>";

        }
    }

    if ($("#GiftType option:selected").text() == "Gift Card" &&
        $("#AltGiftType option:selected").text() == "Gift Card") {
        isValid = false;
        message += "Only one gift wish can be a gift card<br/>";
    }

    if ($("#Age").val() != "") {
        if (($("#GiftType option:selected").text() == "Gift Card" ||
            $("#AltGiftType option:selected").text() == "Gift Card") &&
            $("#Age").val() < 10) {
            isValid = false;
            message += "No child under 10 can receive a gift card.";
        }
    }

    if (isValid) {
        $("#btnSaveSubmit").attr('class', 'btn btn-primary');
        $("#btnSaveEdit").attr('class', 'btn btn-primary');
    }
    else {
        $("#btnSaveSubmit").attr('class', 'btn btn-secondary');
        $("#btnSaveEdit").attr('class', 'btn btn-secondary');
    }

    return { isValid, message };
}

$("#btnSaveSubmit").on('click', function () {
    $("#errorMessage").empty();
    let { isValid, message } = checkValid();
    if (isValid) {
        $('#heartModalBody').empty();
        $('#heart').clone().attr('style', 'margin:auto').appendTo('#heartModalBody');
        $("#heartModal").modal("show");
    } else {
        $("#errorMessage").html(message);
        $("#modalWarning").modal("show");
    }
});

$('#confirm').on('click', function () {
    $("#heartModal").modal("hide");
    var GiftRecipientModel = {
        Location: $("#Location").val(),
        Name: $("#Name").val(),
        Age: $("#Age").val(),
        AgeType: $("#AgeType").val(),
        Gender: $("#Gender").val(),
        RecipientInfo: $("#RecipientInfo").val(),
        GiftWish: $("#GiftWish").val(),
        GiftType: $("#GiftType").val(),
        GiftDetail1: $("#GiftDetail1").val(),
        GiftDetail2: $("#GiftDetail2").val(),
        AltGiftWish: $("#AltGiftWish").val(),
        AltGiftType: $("#AltGiftType").val(),
        AltGiftDetail1: $("#AltGiftDetail1").val(),
        AltGiftDetail2: $("#AltGiftDetail2").val(),
        Status: "20"
    };

    $.ajax({
        dataType: "json",
        url: "/GiftRecipient/AddGiftRecipient",
        data: GiftRecipientModel,
        type: 'POST',
        success: function (data) {
            setTimeout(function () { $("#modalCloseContinue").modal("show"); }, 30);
        },
        error: function (jqXHR, exception) {
            window.location.href = "/Error";
        }

    });
})

$("#btnSaveEdit").on('click', function () {
    $("#errorMessage").empty();
    var { isValid, message } = checkValid();
    if (isValid) {
        var GiftRecipientModel = {
            Location: $("#Location").val(),
            Name: $("#Name").val(),
            Age: $("#Age").val(),
            AgeType: $("#AgeType").val(),
            Gender: $("#Gender").val(),
            RecipientInfo: $("#RecipientInfo").val(),
            GiftWish: $("#GiftWish").val(),
            GiftType: $("#GiftType").val(),
            GiftDetail1: $("#GiftDetail1").val(),
            GiftDetail2: $("#GiftDetail2").val(),
            AltGiftWish: $("#AltGiftWish").val(),
            AltGiftType: $("#AltGiftType").val(),
            AltGiftDetail1: $("#AltGiftDetail1").val(),
            AltGiftDetail2: $("#AltGiftDetail2").val(),
            Status: "10"
        };

        $.ajax({
            dataType: "json",
            url: "/GiftRecipient/AddGiftRecipient",
            data: GiftRecipientModel,
            type: 'POST',
            success: function (data) {
                setTimeout(function () { $("#modalCloseContinue").modal("show") }, 30);
            },
            error: function (jqXHR, exception) {
                window.location.href = "/Error";
            }

        });
    } else {
        $("#errorMessage").html(message);
        $("#modalWarning").modal("show");
    }
});

$("#btnDone").on('click', function () {
    window.location.href = "/GiftRecipient/Index";
});

$("#btnAnother").on('click', function () {
    location.reload(true);
});


function GetNameAgeGenderString() {
    var nameagegenderstring = $("#Name").val() + ' age: ' + $("#Age").val() + 'yrs - ' + $("#Gender").val();
    if ($("#AgeType").val() == "months") {
        nameagegenderstring = $("#Name").val() + ' age: ' + $("#Age").val() + 'm - ' + $("#Gender").val();
    }
    $("#heart_name_age_gender").html(nameagegenderstring);
}

function GetGiftWishString() {
    var giftWishString = "";
    if (!$("#GiftDetail1").length && !$("#GiftDetail2").length) {
        giftWishString = "1st Wish: " + $("#GiftWish").val();
    }
    else if (!$("#GiftDetail2").length) {
        giftWishString = "1st Wish: " + $("#GiftWish").val() + ": " + $("#GiftDetail1").val();
    } else {
        giftWishString = "1st Wish: " + $("#GiftWish").val() + ": " + $("#GiftDetail1").val() + "; " + $("#GiftDetail2").val();
    }
    $("#giftwish").html(giftWishString);
}

function GetAltGiftWishString() {
    var giftWishString = "";
    if (!$("#AltGiftDetail1").length && !$("#AltGiftDetail2").length) {
        giftWishString = "2nd Wish: " + $("#AltGiftWish").val();
    }
    else if (!$("#AltGiftDetail2").length) {
        giftWishString = "2nd Wish: " + $("#AltGiftWish").val() + ": " + $("#AltGiftDetail1").val();
    } else {

        giftWishString = "2nd Wish: " + $("#AltGiftWish").val() + ": " + $("#AltGiftDetail1").val() + "; " + $("#AltGiftDetail2").val();
    }
    $("#altgiftwish").html(giftWishString);
}

function GetGiftDetails(id) {
    var giftDetail = details.find(d => d.giftDetailId == id);
    $("#giftWishDescription").html("");
    if (giftDetail.giftIdeaDescription != null) {
        $("#giftWishDescription").html(giftDetail.giftIdeaDescription);
        if (giftDetail.giftDetailText == "Bike") {
            $("#showSize").css("display", "block");
        }
    }

    $("#giftWishDetails").empty();
    strHtml = "";
    if (giftDetail.lblGiftDetail1 != null) {
        strHtml += "<div class='form-floating mb-3'>"
        strHtml += "<input type='text' id='GiftDetail1' class='form-control' maxlength='15' ></input>";
        strHtml += "<label for='GiftDetail1' class='col-form-label'>" + giftDetail.lblGiftDetail1 + "</label>";
        strHtml += "</div>"
    }

    if (giftDetail.lblGiftDetail2 != null) {
        strHtml += "<div class='form-floating mb-3'>"
        strHtml += "<input type='text' id='GiftDetail2' class='form-control' maxlength='15' ></input>";
        strHtml += "<label for='GiftDetail2' class='col-form-label'>" + giftDetail.lblGiftDetail2 + "</label>";
        strHtml += "</div>"
    }

    $("#giftWishDetails").html(strHtml);

    if (giftDetail.lblGiftDetail1 != null) {
        document.getElementById('GiftDetail1').addEventListener('keyup', function (event) {
            GetGiftWishString();
            checkValid();
        });
    }

    if (giftDetail.lblGiftDetail2 != null) {
        document.getElementById('GiftDetail2').addEventListener('keyup', function (event) {
            GetGiftWishString();
            checkValid();
        });
    }
}

function GetAltGiftDetails(id) {
    var giftDetail = details.find(d => d.giftDetailId == id);
    $("#altGiftWishDescription").html("");
    if (giftDetail.giftIdeaDescription != null) {
        $("#altGiftWishDescription").html(giftDetail.giftIdeaDescription);
        if (giftDetail.giftDetailText == "Bike") {
            $("#altShowSize").css("display", "block");
        }
    }

    $("#altGiftWishDetails").empty();
    strHtml = "";
    if (giftDetail.lblGiftDetail1 != null) {
        strHtml += "<div class='form-floating mb-3'>";
        strHtml += "<input type='text' id='AltGiftDetail1' class='form-control' maxlength='15' ></input>";
        strHtml += "<label for='AltGiftDetail1'>" + giftDetail.lblGiftDetail1 + "</label>";
        strHtml += "</div>"
    }

    if (giftDetail.lblGiftDetail2 != null) {
        strHtml += "<div class='form-floating mb-3'>";
        strHtml += "<input type='text' id='AltGiftDetail2' class='form-control' maxlength='15' ></input>";
        strHtml += "<label for='AltGiftDetail2'>" + giftDetail.lblGiftDetail2 + "</label>";
        strHtml += "</div>"
    }

    $("#altGiftWishDetails").html(strHtml);

    if (giftDetail.lblGiftDetail1 != null) {
        document.getElementById('AltGiftDetail1').addEventListener('keyup', function (event) {
            GetGiftWishString();
            checkValid();
        });
    }
    if (giftDetail.lblGiftDetail2 != null) {
        document.getElementById('AltGiftDetail2').addEventListener('keyup', function (event) {
            GetGiftWishString();
            checkValid();
        });
    }
}

document.getElementById('Age').addEventListener('input', function (e) {
    if (this.value.length > 3) {
        this.value = this.value.slice(0, 3);
    }
});