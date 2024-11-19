using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblStatusLog
{
    public int LogId { get; set; }

    public int LabelNum { get; set; }

    public int StatusId { get; set; }

    public DateTime DateEdited { get; set; }

    public int AppId { get; set; }

    public string? ChangeInfo { get; set; }

    public string? LastSponsorID {  get; set; }

    public virtual TblApp App { get; set; } = null!;

    public virtual TblRecipientChild? LabelNumNavigation { get; set; }

    public virtual TblStatusType Status { get; set; } = null!;
}
