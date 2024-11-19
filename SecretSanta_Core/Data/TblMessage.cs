using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblMessage
{
    public int MessageId { get; set; }

    public string MessageTitle { get; set; } = null!;

    public string? MessageContent { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public virtual ICollection<TblMessageContact> TblMessageContacts { get; } = new List<TblMessageContact>();
}
