using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblRegion
{
    public string RegionShort { get; set; } = null!;

    public string RegionLong { get; set; } = null!;

    public virtual ICollection<TblAgency> TblAgencies { get; } = new List<TblAgency>();

    public virtual ICollection<TblSponsor> TblSponsors { get; } = new List<TblSponsor>();
}
