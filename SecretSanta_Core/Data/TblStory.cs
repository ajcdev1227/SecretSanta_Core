using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblStory
{
    public int StoryId { get; set; }

    public string Title { get; set; } = null!;

    public string StoryContent { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public bool IsActive { get; set; }
}
