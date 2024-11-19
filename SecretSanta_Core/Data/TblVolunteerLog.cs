using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblVolunteerLog
{
    public int VolLogNum { get; set; }

    public int VolId { get; set; }

    public string? FreeField { get; set; }

    public DateTime? TimeOut { get; set; }

    public DateTime? TimeIn { get; set; }

    public string? Company { get; set; }

    public string? Site { get; set; }
}
