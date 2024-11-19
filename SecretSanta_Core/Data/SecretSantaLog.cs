using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class SecretSantaLog
{
    public int Id { get; set; }

    public DateTime LogDateTime { get; set; }

    public string ExceptionType { get; set; } = null!;

    public string ExceptionMessage { get; set; } = null!;

    public string ExceptionSource { get; set; } = null!;

    public string? StackTrace { get; set; }
}
