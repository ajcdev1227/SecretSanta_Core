using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblAdminEmail
{
    public int EmailId { get; set; }

    public string EmailName { get; set; } = null!;

    public string EmailContent { get; set; } = null!;
}
