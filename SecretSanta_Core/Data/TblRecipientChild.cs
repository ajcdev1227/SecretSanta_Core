using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblRecipientChild
{
    public int LabelNum { get; set; }

    public int RecipientNum { get; set; }

    public bool Primary { get; set; }

    public string? HistoryNotes { get; set; }

    public string? BulkEditTracker {  get; set; }

    public string? BulkEditTracker2 {  get; set; }

    public string? RecChildFreefield { get; set; }

    public virtual TblRecipientParent RecipientNumNavigation { get; set; } = null!;

    public virtual ICollection<TblStatusLog> TblStatusLogs { get; } = new List<TblStatusLog>();
}
