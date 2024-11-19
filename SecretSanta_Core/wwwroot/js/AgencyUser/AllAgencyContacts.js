var oldAgencyRole = '';
function editAgency(agency) {
    oldAgencyRole = agency.roles;
    $('#Id').val(agency.id);
    $('#AgencyId').val(agency.agencyId);
    $('#Email').val(agency.email);
    $('#FirstName').val(agency.firstName);
    $('#LastName').val(agency.lastName);
    $('#Roles').val(agency.roles);
    $('#Phone').val(agency.phone);
    $('#AltPhone').val(agency.altPhone);
    $('#Fax').val(agency.fax);
    $('#EstimateWishes').val(agency.estimateWishes);
    $('#IsActive').val(agency.isActive?'true':'false');
    $('#Archive').val(agency.archive?'true':'false');
    console.log(agency.isActive);
    $('#editAgencyModal').modal('show');
}

$('#submit').on('click', function () {
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
    }else if ($('#IsActive').val() == 'false') {
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
    }else if ($('#Roles').val() == "Leader" && oldAgencyRole != "Leader") {
        $.ajax({
            url: "/AgencyUser/checkLeaders",
            data: { "agencyId": $('#AgencyId').val() },
            success: function (data) {
                if (data.count > 0) {
                    swal({
                        text: "A leader already exists, do you want to change his role to Primary?",
                        icon: "info",
                        buttons: true
                    }).then((willChange) => {
                        if (willChange) {
                            $('#agencyForm').submit();
                        }
                    });
                }
            },
            error: function (jqXHR, exception) {
                alert(jqXHR.responseText);
            }
        });
    } else {
        $('#agencyForm').submit();
    }
    
})