$(document).ready(function () {
    var agencyId = $("#agencyId").val();
    if ($("#attentionVal").val() == 1) {
        GetDraftEdit(agencyId);
    } else {
        GetAllRecipientsForAgency(agencyId);
    }

    GetAllGiftsForAgency(agencyId);

    $("#btnInfo").click(function () {
        $("#modalInfo").modal("show");
    });
    $("#add").click(function () {
        window.location.href = "/GiftRecipient/Add";
    });

    $(".chkDashboard").change(function () {
        var statuses = new Array();

        $('input:checkbox[class=chkDashboard]:checked').each(function () {
            statuses.push($(this).val());
        });
        console.log(statuses);
        if (statuses.length > 0) {
            var myJsonString = JSON.stringify(statuses);
            $.ajax({
                dataType: "json",
                data: {
                    webGroups: myJsonString,
                    agencyId: agencyId
                },
                url: '/GiftRecipient/GetRecipientsByStatus',
                success: function (data) {
                    WriteTable(data);
                },
                error: function (jqXHR, exception) {
                    window.location.href = "/Error";
                }
            });
        } else {
            GetAllRecipientsForAgency(agencyId);

        }
    });


    $("#attention").click(function () {
        GetDraftEdit(agencyId);
    });


    $("#table").on("click",
        "a.edit",
        function () {
            var id = $(this).attr("id");
            window.location.href = "/GiftRecipient/Edit/" + id;
        });

    $(".chkGift").change(function () {
        var statuses = new Array();

        $('input:checkbox[class=chkGift]:checked').each(function () {
            statuses.push($(this).val());
        });
        if (statuses.length > 0) {
            var myJsonString = JSON.stringify(statuses);
            $.ajax({
                dataType: "json",
                data: {
                    webGroups: myJsonString,
                    agencyId: agencyId
                },
                url: '/GiftRecipient/GetGiftsByStatus',
                success: function (data) {
                    WriteGiftTable(data);
                },
                error: function (jqXHR, exception) {
                    window.location.href = "/Error";
                }
            });
        } else {
            GetAllGiftsForAgency(agencyId);

        }
    });


});

function GetDraftEdit(agencyId) {
    $.ajax({
        dataType: "json",
        url: '/GiftRecipient/GetDraftEdit',
        data: { "agencyId": agencyId },
        type: 'POST',
        success: function (data) {
            WriteTable(data);
            $(".chkDashboard").each(function () {
                if ($(this).attr("id") == 'draft' || $(this).attr("id") == 'revise') {
                    $(this).prop("checked", true);
                } else {
                    $(this).prop("checked", false);
                }

            });
        },
        error: function (jqXHR, exception) {
            window.location.href = "/Error";
        }
    });
}

function GetAllRecipientsForAgency(agencyId) {
    $.ajax({
        dataType: "json",
        url: '/GiftRecipient/GetAllRecipientsForAgency',
        data: { "agencyId": agencyId },
        type: 'POST',
        success: function (data) {
            WriteTable(data);
        },
        error: function (jqXHR, exception) {
            window.location.href = "/Error";
        }
    });
}

function GetAllGiftsForAgency(agencyId) {
    $.ajax({
        dataType: "json",
        url: '/GiftRecipient/GetAllGiftsForAgency',
        data: { "agencyId": agencyId },
        type: 'POST',
        success: function (data) {
            WriteGiftTable(data);
        },
        error: function (jqXHR, exception) {
            window.location.href = "/Error";
        }
    });
}

function WriteTable(data) {
    $("#table").empty();
    var stringHtml = "<table class='table table-striped'>";
    stringHtml += "<tr><th>Recipient Num</th><th>Name</th><th>Age</th><th>Gender</th><th>Info</th><th>First Wish</th><th>Second Wish</th><th>Status</th><th>Edit Notes</th><th></th></tr>";
    if (data.result.length > 0) {
        for (var i = 0; i < data.result.length; i++) {
            var age = data.result[i].age != null && data.result[i].ageType != null ? data.result[i].age + " " + data.result[i].ageType : "";
            var gender = data.result[i].gender != null ? data.result[i].gender : "";
            var info = data.result[i].recipientInfo != null ? data.result[i].recipientInfo : "";
            var giftWish = data.result[i].giftWish != null ? data.result[i].giftWish : "";
            var altGiftWish = data.result[i].altGiftWish != null ? data.result[i].altGiftWish : "";
            var editNotes = data.result[i].editNotes != null ? data.result[i].editNotes : "";
            stringHtml += "<tr>";
            stringHtml += "<td>" + data.result[i].recipientNum + "</td>";
            stringHtml += "<td>" + data.result[i].name + "</td>";
            stringHtml += "<td>" + age + "</td>";
            stringHtml += "<td>" + gender + "</td>";
            stringHtml += "<td>" + info + "</td>";
            stringHtml += "<td>" + giftWish + "</td>";
            stringHtml += "<td>" + altGiftWish + "</td>";
            stringHtml += "<td>" + data.result[i].status + "</td>";
            stringHtml += "<td>" + editNotes + "</td>";
            if (data.result[i].status == "Draft" || data.result[i].status == "New" || data.result[i].status == "Edit") {
                stringHtml += "<td><a href='#' class='edit' id='" + data.result[i].recipientNum + "'>Edit Heart Wish</a></td>";
            }
            stringHtml += "</tr>";
        }
    }
    stringHtml += "</table>";
    $("#table").append(stringHtml);
}

function WriteGiftTable(data) {
    $("#giftTable").empty();
    var stringHtml = "<table class='table table-striped'>";
    stringHtml += "<tr><th>Location</th><th>Recipient Num</th><th>Label Num</th><th>Name</th><th>Age</th><th>Gender</th><th>Info</th><th>First Wish</th><th>Second Wish</th><th>Status</th><th>Edit Notes</th></tr>";
    if (data.gifts.length > 0) {
        for (var i = 0; i < data.gifts.length; i++) {
            var age = data.gifts[i].age != null && data.gifts[i].ageType != null ? data.gifts[i].age + " " + data.gifts[i].ageType : "";
            var gender = data.gifts[i].gender != null ? data.gifts[i].gender : "";
            var info = data.gifts[i].recipientInfo != null ? data.gifts[i].recipientInfo : "";
            var giftWish = data.gifts[i].giftWish != null ? data.gifts[i].giftWish : "";
            var altGiftWish = data.gifts[i].altGiftWish != null ? data.gifts[i].altGiftWish : "";
            var editNotes = data.gifts[i].editNotes != null ? data.gifts[i].editNotes : "";
            var location = data.gifts[i].location != null ? data.gifts[i].location : "";
            stringHtml += "<tr>";
            stringHtml += "<td>" + location + "</td>";
            stringHtml += "<td>" + data.gifts[i].recipientNum + "</td>";
            stringHtml += "<td>" + data.gifts[i].labelNum + "</td>";
            stringHtml += "<td>" + data.gifts[i].name + "</td>";
            stringHtml += "<td>" + age + "</td>";
            stringHtml += "<td>" + gender + "</td>";
            stringHtml += "<td>" + info + "</td>";
            stringHtml += "<td>" + giftWish + "</td>";
            stringHtml += "<td>" + altGiftWish + "</td>";
            stringHtml += "<td>" + data.gifts[i].status + "</td>";
            stringHtml += "<td>" + editNotes + "</td>";
            stringHtml += "</tr>";
        }
    }
    stringHtml += "</table>";
    $("#giftTable").append(stringHtml);
}
