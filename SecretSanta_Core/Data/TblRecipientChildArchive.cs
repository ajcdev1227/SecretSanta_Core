using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblRecipientChildArchive
{
    public int LabelNum { get; set; }

    public int RecipientNum { get; set; }

    public bool BigItem { get; set; }

    public bool BikeReceived { get; set; }

    public bool GiftCard { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public bool Primary { get; set; }

    public string Year { get; set; } = null!;

    public int? GiftCount { get; set; }

    public int? LastStatus { get; set; }

    public string? FreeField { get; set; }

    public string? BulkStatusChange { get; set; }

    public string? HistoryNotes { get; set; }

    public string? EditNewRecipient { get; set; }

    public string? PrintHeart { get; set; }

    public string? PrintTag { get; set; }

    public string? BulkEdit { get; set; }

    public string? ConfirmHeartPrint { get; set; }

    public string? ConfirmTagPrint { get; set; }

    public bool? IsActive { get; set; }

    public string? HeartCentralGiftCode { get; set; }

    public virtual TblRecipientParentArchive RecipientNumNavigation { get; set; } = null!;

    public virtual ICollection<TblStatusLogArchive> TblStatusLogArchives { get; } = new List<TblStatusLogArchive>();
}
