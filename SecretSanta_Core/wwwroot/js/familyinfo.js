$(document).ready(function () {
    $("#previous").click(function () {
        window.location.href = "/SantaLetter/WriterInfo";
    });
});

function checkValid() {
    var sum = Number($("#NumChildrenUnder19").val()) +
        Number($("#NumChildrenOver19").val()) +
        Number($("#NumParents").val()) +
        Number($("#NumGrandparents").val()) +
        Number($("#NumOtherFamily").val());

    if (sum > 0) {
        $("#next").attr('class', 'btn btn-primary');
        return true;
    }

    else {
        $("#next").attr('class', 'btn btn-secondary');
        return false;
    }
        
}

$('#NumChildrenUnder19, #NumChildrenOver19, #NumParents, #NumGrandparents, #NumOtherFamily').on('change, keyup', function () {
    checkValid();
})

$("#next").click(function () {
    if (checkValid()) {
        $('form#familyInfo').submit();
    } else {
        swal({
            title: "",
            text: "Please enter a value of at least 0 for each box above. At least one of them has to have an over 0 in it.",
            type: "warning"
        });
    }
});