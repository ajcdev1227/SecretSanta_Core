using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblApp
{
    public int AppId { get; set; }

    public string AppName { get; set; } = null!;

    public byte[] RowVersion { get; set; } = null!;

    public int? SetStatusTo { get; set; }

    public string? LaunchForm { get; set; }

    public bool? IncludeInLogin { get; set; }

    public string? ChangeInfo { get; set; }

    public virtual ICollection<TblStatusLogArchive> TblStatusLogArchives { get; } = new List<TblStatusLogArchive>();

    public virtual ICollection<TblStatusLog> TblStatusLogs { get; } = new List<TblStatusLog>();
}
