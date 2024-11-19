using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblDonationArchive
{
    public string DonNumber { get; set; }

    public int DonRecipientNum { get; set; }

    public decimal DonAmount { get; set; }

    public DateTime? DonDateTime { get; set; }

    public int? Year { get; set; }
}
