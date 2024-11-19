using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblDonorThankYou
{
    public int DonorThankYouId { get; set; }

    public string DonorId { get; set; } = null!;

    public string? Message { get; set; }

    public string? Image { get; set; }

    public DateTime ThankYouDate { get; set; }

    public int RecipientNum { get; set; }

    public bool Approved { get; set; }

    public virtual TblDonor Donor { get; set; } = null!;
}
