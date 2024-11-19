using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblViewItemsSponsor
{
    public int ViewSponsorKey { get; set; }

    public int ViewId { get; set; }

    public int SponsorId { get; set; }

    public virtual TblSponsor Sponsor { get; set; } = null!;

    public virtual TblViewHeader View { get; set; } = null!;
}
