using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblAppUser
{
    public int AppUserAu { get; set; }

    public int AuuserId { get; set; }

    public int AuappId { get; set; }

    public string Aupermission { get; set; } = null!;

    public string Austatus { get; set; } = null!;
}
