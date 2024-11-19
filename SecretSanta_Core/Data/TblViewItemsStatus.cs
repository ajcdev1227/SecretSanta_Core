using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblViewItemsStatus
{
    public int ViewStatusKey { get; set; }

    public int ViewId { get; set; }

    public int StatusId { get; set; }

    public virtual TblStatusType Status { get; set; } = null!;

    public virtual TblViewHeader View { get; set; } = null!;
}
