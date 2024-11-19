using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblViewItemsAgency
{
    public int ViewAgencyKey { get; set; }

    public int ViewId { get; set; }

    public int AgencyId { get; set; }

    public virtual TblAgency Agency { get; set; } = null!;

    public virtual TblViewHeader View { get; set; } = null!;
}
