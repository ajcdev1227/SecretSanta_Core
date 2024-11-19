using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblErrorLogArchive
{
    public int ErrorId { get; set; }

    public DateTime ErrorDate { get; set; }

    public int? AppId { get; set; }

    public int? AppUserId { get; set; }

    public int? StatusId { get; set; }

    public string? ErrorDesc { get; set; }

    public string? Form { get; set; }

    public string? Field { get; set; }

    public string? Event { get; set; }

    public int? LabelId { get; set; }

    public string? MiscVariable { get; set; }

    public string? Code { get; set; }

    public string? Machine { get; set; }

    public string Year { get; set; } = null!;
}
