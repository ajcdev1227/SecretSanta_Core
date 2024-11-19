using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblElf
{
    public int ElfId { get; set; }

    public string ElfFirstName { get; set; } = null!;

    public string? ElfLastName { get; set; }

    public string? ElfEmail { get; set; }

    public string? ElfPhone { get; set; }

    public string? ElfAltPhone { get; set; }

    public string? Notes { get; set; }

    public string? TrainingDates { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<TblSponsor> TblSponsors { get; } = new List<TblSponsor>();
}
