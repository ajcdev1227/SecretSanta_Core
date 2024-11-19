using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblUser
{
    public int UserId { get; set; }

    public string UserFirstName { get; set; } = null!;

    public string UserLastName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public DateTime UserDateCreated { get; set; }

    public bool UserAdmin { get; set; }

    public bool? UserIsActive { get; set; }

    public string? CellPhone { get; set; }

    public string? Email { get; set; }

    public bool? UserMessage { get; set; }

    public virtual ICollection<TblStatusLogArchive> TblStatusLogArchives { get; } = new List<TblStatusLogArchive>();

    public virtual ICollection<TblViewHeader> TblViewHeaders { get; } = new List<TblViewHeader>();
}
