using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblRole
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public byte[] RowVersion { get; set; } = null!;
}
