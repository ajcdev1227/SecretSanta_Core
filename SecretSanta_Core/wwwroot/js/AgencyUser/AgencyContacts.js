var agencyRole = null;
function editAgency(agency) {
    $('#Id').val(agency.id);
    $('#Email').val(agency.email);
    $('#FirstName').val(agency.firstName);
    $('#LastName').val(agency.lastName);
    $('#Phone').val(agency.phone);
    $('#AltPhone').val(agency.altPhone);
    $('#Fax').val(agency.fax);
    $('#EstimateWishes').val(agency.estimateWishes);
    agencyRole = agency.roles;
    $('#editAgencyModal').modal('show');
}

$('#submit').on('click', function () {
    if (agencyRole == "Leader" && ($('#Archive').val() == 'true' || $('#IsActive').val() == 'false')) {
        swal({
            text: "You can't edit a leader's active status.",
            icon: "warning",
            buttons: true
        });
        return;
    }
    if ($('#Archive').val() == 'true') {
        swal({
            text: "Archive will cause this user to no longer be visible, contact secret santa admin to change it back",
            icon: "warning",
            buttons: ["Leave as is, return to prior screen", "Ok to proceed"]
        }).then((willChange) => {
            if (willChange) {
                $('#agencyForm').submit();
            } else {
                return;
            }
        });
    } else if ($('#IsActive').val() == 'false') {
        swal({
            text: "This user will not be permitted to log in, you can change this back later if you wish",
            icon: "warning",
            buttons: ["Leave as is, return to prior screen", "Ok to proceed"]
        }).then((willChange) => {
            if (willChange) {
                $('#agencyForm').submit();
            } else {
                return;
            }
        });
    } else {
        $('#agencyForm').submit();
    }
})