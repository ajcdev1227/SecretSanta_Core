$(document).ready(function () {
    GetNameAgeGenderString();
    $("#heart_recipient_info").html($("#RecipientInfo").val());

    var details = new Array();

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

            var id = $("#GiftType").val();
            if (id != "") {
                GetExistingGiftWishDetail(id);
            }
            var altId = $("#AltGiftType").val();
            if (altId != "") {
                GetExistingAltGiftDetail(altId);
            }
        },
        error: function (jqXHR, exception) {
            alert(jqXHR.responseText);
        }


    });

    $("#GiftType").change(function () {
        $("#showSize").css("display", "none");
        var v = $(this).val();
        if (v != "") {
            GetGiftDetails(v);
        }
        if ($("#GiftType option:selected").text() == "Gift Card") {
            $("#GiftWish").val("Gift Card");
        } else {
            $("#GiftWish").val("");
        }
        $("#giftwish").empty();
    });

    $("#AltGiftType").change(function () {
        $("#altShowSize").css("display", "none");
        var v = $(this).val();
        if (v != "") {
            GetAltGiftDetails(v);
        }
        if ($("#AltGiftType option:selected").text() == "Gift Card") {
            $("#AltGiftWish").val("Gift Card");
        } else {
            $("#AltGiftWish").val("");
        }
        $("#altgiftwish").empty();

    });

    $("#showSize").on("click",
        function () {
            $("#mdl").modal("show");
        });

    $("#altShowSize").on("click",
        function () {
            $("#mdl").modal("show");
        });

    $("#giftWishDetails").on("change",
        "input[id=gift1]",
        function () {
            $("#GiftDetail1").val($(this).val());
        });

    $("#giftWishDetails").on("change",
        "input[id=gift2]",
        function () {
            $("#GiftDetail2").val($(this).val());
        });

    $("#altGiftWishDetails").on("change",
        "input[id = altgift1]",
        function () {
            $("#AltGiftDetail1").val($(this).val());
        });

    $("#altGiftWishDetails").on("change",
        "input[id=altgift2]",
        function () {
            $("#AltGiftDetail2").val($(this).val());
        });

    $("#btnNameInfo").click(function () {
        $("#modalNameInfo").modal("show");
    });

    $(".info-wish").click(function () {
        $("#modalWishInfo").modal("show");
    });

    $("#btnRecipientInfo").click(function () {
        $("#modalRecipientInfo").modal("show");
    });

    $(".name_age_gender").keyup(function () {
        GetNameAgeGenderString();
    });

    $(".name_age_gender_dropdown").change(function () {
        GetNameAgeGenderString();
    });

    $("#RecipientInfo").keyup(function () {
        $("#heart_recipient_info").html($("#RecipientInfo").val());
    });

    $("#GiftWish").keyup(function () {
        GetGiftWishString();
    });

    $(document).on("keyup",
        "#Detail1",
        function () {
            GetGiftWishString();
        });

    $(document).on("keyup",
        "#Detail2",
        function () {
            GetGiftWishString();
        });
    $("#AltGiftWish").keyup(function () {
        GetAltGiftWishString();
    });

    $(document).on("keyup",
        "#AltDetail1",
        function () {
            GetAltGiftWishString();
        });

    $(document).on("keyup",
        "#AltDetail2",
        function () {
            GetAltGiftWishString();
        });


    $("#btnSaveSubmit")
        .click(function () {
            $("#errorMessage").empty();
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
                if (giftDetail.lblGiftDetail1 != null && $("#Detail1").val() == "") {
                    isValid = false;
                    message += "Detail is required<br/>";

                }
                if (giftDetail.lblGiftDetail2 != null && $("#Detail2").val() == "") {
                    isValid = false;
                    message += "Detail is required<br/>";

                }
            }

            ida = $("#AltGiftType").val();
            if (ida != "") {
                var giftDetail = details.find(d => d.giftDetailId == ida);
                if (giftDetail.lblGiftDetail1 != null && $("#AltDetail1").val() == "") {
                    isValid = false;
                    message += "Detail is required<br/>";

                }
                if (giftDetail.lblGiftDetail2 != null && $("#AltDetail2").val() == "") {
                    isValid = false;
                    message += "Detail is required<br/>";

                }
            }

            if ($("#GiftType option:selected").text() == "Gift Card" &&
                $("#AltGiftType option:selected").text() == "Gift Card") {
                isValid = "false";
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
                $(this).attr("disabled", "disabled");
                var GiftRecipientModel = {
                    Location: $("#Location").val(),
                    RecipientNum: $("#RecipientNum").val(),
                    Name: $("#Name").val(),
                    Age: $("#Age").val(),
                    AgeType: $("#AgeType").val(),
                    Gender: $("#Gender").val(),
                    RecipientInfo: $("#RecipientInfo").val(),
                    GiftWish: $("#GiftWish").val(),
                    GiftType: $("#GiftType").val(),
                    GiftDetail1: $("#Detail1").val(),
                    GiftDetail2: $("#Detail2").val(),
                    AltGiftWish: $("#AltGiftWish").val(),
                    AltGiftType: $("#AltGiftType").val(),
                    AltGiftDetail1: $("#AltDetail1").val(),
                    AltGiftDetail2: $("#AltDetail2").val(),
                    Status: "20"
                };

                $.ajax({
                    dataType: "json",
                    url: "/GiftRecipient/EditGiftRecipient",
                    data:
                        GiftRecipientModel,
                    type: 'POST',
                    success: function (data) {
                        window.location.href = "/GiftRecipient/Index";
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

    $("#btnSaveEdit")
        .click(function () {
            $("#errorMessage").empty();
            var message = "";
            var isValid = true;
            if ($("#Name").val() == "") {
                isValid = false;
                message += "Name is required";
            }
            if ($("#GiftType option:selected").text() == "Gift Card" &&
                $("#AltGiftType option:selected").text() == "Gift Card") {
                isValid = "false";
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
            if ($("#locationNecessary").val() == "True" && $("#Location").val() == "") {
                isValid = false;
                message += "Location is required<br/>";
            }
            if (isValid) {
                $(this).attr("disabled", "disabled");
                var GiftRecipientModel = {
                    Location: $("#Location").val(),
                    RecipientNum: $("#RecipientNum").val(),
                    Name: $("#Name").val(),
                    Age: $("#Age").val(),
                    AgeType: $("#AgeType").val(),
                    Gender: $("#Gender").val(),
                    RecipientInfo: $("#RecipientInfo").val(),
                    GiftWish: $("#GiftWish").val(),
                    GiftType: $("#GiftType").val(),
                    GiftDetail1: $("#Detail1").val(),
                    GiftDetail2: $("#Detail2").val(),
                    AltGiftWish: $("#AltGiftWish").val(),
                    AltGiftType: $("#AltGiftType").val(),
                    AltGiftDetail1: $("#AltDetail1").val(),
                    AltGiftDetail2: $("#AltDetail2").val(),
                    Status: "10"
                };

                $.ajax({
                    dataType: "json",
                    url: "/GiftRecipient/EditGiftRecipient",
                    data:
                        GiftRecipientModel,
                    type: 'POST',
                    success: function (data) {
                        window.location.href = "/GiftRecipient/Index";
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


    function GetNameAgeGenderString() {
        var nameagegenderstring =
            "Name:" + ' ' + $("#Name").val() +
            ' Age: ' +
            $("#Age").val() +
            ' - ' +
            $("#Gender").val();
        if ($("#AgeType").val() == "months") {
            nameagegenderstring =
                "Name:" + ' ' + $("#Name").val() +
                ' Age: ' +
                $("#Age").val() +
                'm - ' +
                $("#Gender").val();

        }
        $("#heart_name_age_gender").html(nameagegenderstring);
    }

    function GetGiftWishString() {
        var giftWishString = "";
        var giftDetail1 = "";
        if (!$("Detail1").length && !$("#Detail2").length) {
            giftWishString = "First Wish: " + $("#GiftWish").val();
        }
        else if (!$("#Detail2").length) {
            ;
            giftWishString = "First Wish: " + $("#GiftWish").val() + ": " + $("#Detail1").val();
        } else {
            giftWishString = "First Wish: " + $("#GiftWish").val() + ": " + $("#Detail1").val() + "; " + $("#Detail2").val();
        }
        $("#giftwish").html(giftWishString);
    }

    function GetAltGiftWishString() {
        var giftWishString = "";
        var giftDetail1 = "";
        if (!$("#AltDetail1").length && !$("#AltDetail2").length) {
            giftWishString = "Second Wish: " + $("#AltGiftWish").val();
        }
        else if (!$("#AltDetail2").length) {
            giftWishString = "Second Wish: " + $("#AltGiftWish").val() + ": " + $("#AltDetail1").val();
        } else {

            giftWishString = "Second Wish: " + $("#AltGiftWish").val() + ": " + $("#AltDetail1").val() + "; " + $("#AltDetail2").val();
        }
        $("#altgiftwish").html(giftWishString);
    }

    function GetGiftDetails(id) {
        var giftDetail = details.find(d => d.giftDetailId == id);
        $("#giftWishDescription").html("");
        if (giftDetail.giftIdeaDescription != null) {
            $("#giftWishDescription").html(giftDetail.GiftIdeaDescription);
            if (giftDetail.GiftDetailText == "Bike") {
                $("#showSize").css("display", "block");
            }

        }
        $("#giftWishDetails").empty();
        strHtml = "";
        if (giftDetail.lblGiftDetail1 != null) {
            strHtml += "<div>";
            strHtml += "<label for='GiftDetail1' class='col-form-label'>" +
                giftDetail.lblGiftDetail1 +
                "</label>";
            strHtml += "<input type='text' id='Detail1' class='form-control' maxlength='15'></input>";
            strHtml += "</div>";


        }
        if (giftDetail.lblGiftDetail2 != null) {
            strHtml += "<div>";
            strHtml += "<label for='GiftDetail2' class='col-form-label'>" +
                giftDetail.lblGiftDetail2 +
                "</label>";
            strHtml += "<input type='text' id='Detail2' class='form-control' maxlength='15'></input>";
            strHtml += "</div>";


        }
        $("#giftWishDetails").html(strHtml);

    }

    function GetAltGiftDetails(id) {

        $("#altGiftWishDescription").html("");
        var giftDetail = details.find(d => d.giftDetailId == id);
        if (giftDetail.giftIdeaDescription != null) {
            $("#altGiftWishDescription").html(giftDetail.giftIdeaDescription);
            if (giftDetail.GiftDetailText == "Bike") {
                $("#altShowSize").css("display", "block");
            }

        }
        $("#altGiftWishDetails").empty();
        strHtml = "";
        if (giftDetail.lblGiftDetail1 != null) {
            strHtml += "<div>";
            strHtml += "<label for='AltDetail1' class='col-form-label'>" +
                giftDetail.lblGiftDetail1 +
                "</label>";;
            strHtml += "<input type='text' id='AltDetail1' maxlength='15' class='form-control'></input>";
            strHtml += "</div>";


        }
        if (giftDetail.lblGiftDetail2 != null) {
            strHtml += "<div>";
            strHtml += "<label for='AltGiftDetail2' class='col-form-label'>" +
                giftDetail.lblGiftDetail2 +
                "</label>";
            strHtml += "<input type='text' id='AltDetail2' maxlength='15' class='form-control'></input>";
            strHtml += "</div>";


        }
        $("#altGiftWishDetails").html(strHtml);


    }

    function GetExistingGiftWishDetail(id) {
        var giftDetail = details.find(d => d.giftDetailId == id);
        $("#giftWishDescription").html("");
        if (giftDetail.giftIdeaDescription != null) {
            $("#giftWishDescription").html(giftDetail.giftIdeaDescription);
            if (giftDetail.giftDetailText == "Bike") {
                $("#showSize").css("display", "block");
            }

        }
        strHtml = "";
        if (giftDetail.lblGiftDetail1 != null) {
            strHtml += "<div>";
            strHtml += "<label for='GiftDetail1' class='col-form-label'>" +
                giftDetail.lblGiftDetail1 +
                "</label>";
            strHtml += "<input type='text' id='Detail1' class='form-control' maxlength='15' value = '" + $("#GiftDetail1").val() + "'></input>";
            strHtml += "</div>";


        }
        if (giftDetail.lblGiftDetail2 != null) {
            strHtml += "<div>";
            strHtml += "<label id='GiftDetail2' class='col-form-label'>" +
                giftDetail.lblGiftDetail2 +
                "</label>";
            strHtml += "<input type='text' id='Detail2' class='form-control' maxlength='15' value = '" + $("#GiftDetail2").val() + "'></input>";
            strHtml += "</div>";
        }
        $("#giftWishDetails").html(strHtml);
        GetGiftWishString();
    }

    function GetExistingAltGiftDetail(id) {
        var giftDetail = details.find(d => d.giftDetailId == id);
        $("#altGiftWishDescription").html("");
        if (giftDetail.giftIdeaDescription != null) {
            $("#altGiftWishDescription").html(giftDetail.giftIdeaDescription);
            if (giftDetail.giftDetailText == "Bike") {
                $("#altShowSize").css("display", "block");
            }

        }
        strHtml = "";
        if (giftDetail.lblGiftDetail1 != null) {
            strHtml += "<div>";
            strHtml += "<label for='" +
                "AltGiftDetail1' class='col-form-label'>" +
                giftDetail.lblGiftDetail1 +
                "</label>";
            strHtml += "<input type='text' id='AltDetail1' class='form-control' value = '" + $("#AltGiftDetail1").val() + "'></input>";
            strHtml += "</div>";
        }
        if (giftDetail.lblGiftDetail2 != null) {
            strHtml += "<div>";
            strHtml += "<label for='AltGiftDetail2' class='col-form-label'>" +
                giftDetail.lblGiftDetail2 +
                "</label>";
            strHtml += "<input type='text' id='AltDetail2' class='form-control' value = '" + $("#AltGiftDetail2").val() + "'></input>";
            strHtml += "</div>";

        }
        $("#altGiftWishDetails").html(strHtml);
        GetAltGiftWishString();
    }
});

