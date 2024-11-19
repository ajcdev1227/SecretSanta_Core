using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblLetterStatus
{
    public int LetterStatusId { get; set; }

    public int LetterId { get; set; }

    public int StatusId { get; set; }

    public string? StatusNote { get; set; }

    public DateTime DateEdited { get; set; }

    public string? EditedByUser { get; set; }

    public virtual TblLetterSanta Letter { get; set; } = null!;

    public virtual TblStatusType Status { get; set; } = null!;
}
