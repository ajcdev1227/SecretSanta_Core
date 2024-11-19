using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblDonation
{
    public int DonId { get; set; }

    public int RecipientNum { get; set; }

    public decimal Amount { get; set; }

    public DateTime? DonDateTime { get; set; }

    public virtual TblRecipientParent TblRecipientParent { get; set; } = null!;
}
