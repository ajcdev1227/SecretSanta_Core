using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblStatusLogArchive
{
    public int LogId { get; set; }

    public int? LabelNum { get; set; }

    public int StatusId { get; set; }

    public DateTime DateEdited { get; set; }

    public string? EditedByUser { get; set; }

    public int AppId { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public string? ChangeInfo { get; set; }

    public int? EditedByAppUser { get; set; }

    public DateTime? EditedByAppDate { get; set; }

    public int? FreeField { get; set; }

    public string? Machine { get; set; }

    public string Year { get; set; } = null!;

    public string? LastSponsorID { get; set; }

    public virtual TblApp App { get; set; } = null!;

    public virtual TblUser? EditedByAppUserNavigation { get; set; }

    public virtual TblRecipientChildArchive? LabelNumNavigation { get; set; }

    public virtual TblStatusType Status { get; set; } = null!;
}
