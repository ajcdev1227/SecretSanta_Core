using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblVolunteerName
{
    public int VolId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public string? FreeField { get; set; }
}
