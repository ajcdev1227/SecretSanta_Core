$(document).ready(function () {

    $('#modalLabels').modal('show');

    var settings = {
        barWidth: 2,
        barHeight: 40
    };

    $('#numberGifts').on('keyup', function () {
        if ($('#numberGifts').val() != '' && $('#heartNumber').val() != '') {
            $("#printLabels").prop('disabled', false);
        } else {
            $("#printLabels").prop('disabled', true);
        }
    })

    $('#heartNumber').on('keyup', function () {
        if ($('#numberGifts').val() != '' && $('#heartNumber').val() != '') {
            $("#printLabels").prop('disabled', false);
        } else {
            $("#printLabels").prop('disabled', true);
        }
    })

    $("#printLabels").click(function () {
        $('#modalLabels').modal('hide');
        $.ajax({
            dataType: "json",
            data: {
                "numberGifts": $("#numberGifts").val(),
                "heartNumber": $("#heartNumber").val()
            },
            url: '/GiftInstructions/GetLabels',
            success: function (data) {
                var labels = data.labels;
                var labelCount = data.labelCount;

                WritePage(labels, labelCount);
                $(".barcode").each(function () {
                    var labelNum = $(this).prev("input").val();
                    $(this).barcode(
                        labelNum, // Value barcode (dependent on the type of barcode)
                        "code39", // type (string)
                        settings
                    );
                });
            }
        });
    });

    $('#btnPrint').on('click',
        function () {
            window.print();

        });

});

function WritePage(labels, labelCount) {
    var stringHtml = "";
    if (labels.length > 0) {
        for (var i = 0; i < labels.length; i++) {
            var num = i + labelCount + 1;
            stringHtml += "<div class='spacer'></div>";
            stringHtml += "<div>";
            stringHtml += "<div class='label-style'>";
            stringHtml += "<div>Recipient Number: " + labels[i].recipientNumber + "</div>";
            stringHtml += "<div class='center'>";
            stringHtml += "  <div><span class='bold big-text'>" +
                labels[i].name +
                "</span>&nbsp;&nbsp;<span>Age:" +
                labels[i].age +
                "</span>&nbsp;&nbsp;<span class='bold very-big-text'>" +
                labels[i].agencyCode +
                " &nbsp;</span ><span class='big-text'>" +
                labels[i].location +
                "</span></div >";
            stringHtml += "<div><span class='md-text bold'>Label</span>&nbsp;&nbsp;<span class='very-big-text' >" + labels[i].labelNum + "</span ></div>";
            stringHtml += "</div>";
            stringHtml += "<input type='hidden' value='" + labels[i].labelNum + "'></input>";
            stringHtml += "<div class='barcode'></div>";
            stringHtml += "<div style=float:left>Printed&nbsp;" + $("#now").val() + "</div>";
            stringHtml += "<div style=float:right>Gift&nbsp;" + num + "&nbsp;of&nbsp;" + $("#numberGifts").val() + "</div>";
            stringHtml += "<div style='clear:both'></div>";
            stringHtml += "</div></div>";
        }
    } else {
        stringHtml = "It is possible you mis-entered the Recipient Number or Label Number, please try again. If problems continue, please contact Secret Santa. You can find the contact information on the website at <a href='https://secretsantanow.org/LearnMore/Contact'>Contact Us</a>.";
    }
    $("#wrapper").append(stringHtml);
}