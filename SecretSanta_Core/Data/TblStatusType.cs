using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblStatusType
{
    public int StatusId { get; set; }

    public string Status { get; set; } = null!;

    public string? StatusDescription { get; set; }

    public int? StatusSortNumber { get; set; }

    public bool StatusObsolete { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public bool? RecipientChild { get; set; }

    public bool? Letters { get; set; }

    public string? ChartGroup1 { get; set; }

    public string? ChartGroup2 { get; set; }

    public string? WebGroup { get; set; }

    public string? RecipientWebGroup { get; set; }

    public virtual ICollection<TblLetterStatus> TblLetterStatuses { get; } = new List<TblLetterStatus>();

    public virtual ICollection<TblLetterStatusArchive> TblLetterStatusArchives { get; } = new List<TblLetterStatusArchive>();

    public virtual ICollection<TblStatusLogArchive> TblStatusLogArchives { get; } = new List<TblStatusLogArchive>();

    public virtual ICollection<TblStatusLog> TblStatusLogs { get; } = new List<TblStatusLog>();

    public virtual ICollection<TblViewItemsStatus> TblViewItemsStatuses { get; } = new List<TblViewItemsStatus>();
}
