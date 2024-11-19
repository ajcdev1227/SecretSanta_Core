using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblLetterStatusArchive
{
    public int LetterStatusId { get; set; }

    public int LetterId { get; set; }

    public int StatusId { get; set; }

    public string? StatusNote { get; set; }

    public DateTime DateEdited { get; set; }

    public string? EditedByUser { get; set; }

    public string Year { get; set; }

    public virtual TblLetterSantaArchive Letter { get; set; } = null!;

    public virtual TblStatusType Status { get; set; } = null!;
}
