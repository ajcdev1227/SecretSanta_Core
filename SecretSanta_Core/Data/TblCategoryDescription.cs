using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblCategoryDescription
{
    public int CategoryId { get; set; }

    public string Category { get; set; } = null!;

    public string Description { get; set; } = null!;

    public byte[] RowVersion { get; set; } = null!;
}
