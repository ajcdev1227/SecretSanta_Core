using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblAgencyCheckInLog
{
    public int CheckInId { get; set; }

    public string CheckInAgencyContactId { get; set; } = null!;

    public DateTime CheckInDate { get; set; }

    public int CheckInTixNum { get; set; }

    public string? Location { get; set; }
}
