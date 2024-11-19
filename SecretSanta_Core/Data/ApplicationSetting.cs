using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class ApplicationSetting
{
    public int SettingsId { get; set; }

    public string SettingsName { get; set; } = null!;

    public string SettingsValue { get; set; } = null!;

    public string Category { get; set; } = null!;
}
