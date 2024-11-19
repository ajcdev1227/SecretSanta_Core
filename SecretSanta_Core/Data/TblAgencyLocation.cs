using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblAgencyLocation
{
    public int TblAgencyLocationId { get; set; }

    public int AgencyId { get; set; }

    public string Location { get; set; } = null!;

    public string? LocDesc { get; set; }

    public virtual TblAgency Agency { get; set; } = null!;
}
