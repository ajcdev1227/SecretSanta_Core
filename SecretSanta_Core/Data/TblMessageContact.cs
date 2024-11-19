using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblMessageContact
{
    public int MessageId { get; set; }

    public string ContactId { get; set; } = null!;

    public bool Accepted { get; set; }

    public virtual TblAgencyContact Contact { get; set; } = null!;

    public virtual TblMessage Message { get; set; } = null!;
}
