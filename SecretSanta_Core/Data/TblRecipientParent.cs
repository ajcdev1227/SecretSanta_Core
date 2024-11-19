using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblRecipientParent
{
    public int RecipientNum { get; set; }

    public string? Name { get; set; }

    public int? Age { get; set; }

    public string? AgeType { get; set; }

    public string? Gender { get; set; }

    public string? RecipientInfo { get; set; }

    public string? GiftWish { get; set; }

    public int? GiftType { get; set; }

    public string? GiftDetail1 { get; set; }

    public string? GiftDetail2 { get; set; }

    public string? AltGiftWish { get; set; }

    public int? AltGiftType { get; set; }

    public string? AltGiftDetail1 { get; set; }

    public string? AltGiftDetail2 { get; set; }

    public int? AgencyId { get; set; }

    public bool? Resubmit { get; set; }

    public string? EditNotes { get; set; }

    public string? Location { get; set; }

    public bool? IsActive { get; set; }

    public string? DonorId { get; set; }

    public DateTime? DonorRegisterDate { get; set; }

    public DateTime? DateEntered { get; set; }

    public int? WebRank { get; set; }

    public int? FloorGiftCount { get; set; }

    public int? GiftCardCount { get; set; }

    public int? BikeCount { get; set; }

    public int? TotalGiftCount { get; set; }

    public string? PrintTracker { get; set; }

    public string? ApproveTracker { get; set; }

    public string? RecParFreefield { get; set; }

    public virtual TblAgency? Agency { get; set; }

    public virtual GiftDetail? AltGiftTypeNavigation { get; set; }

    public virtual TblDonor? Donor { get; set; }

    public virtual GiftDetail? GiftTypeNavigation { get; set; }

    public virtual ICollection<TblDonation> TblDonations { get; } = new List<TblDonation>();

    public virtual ICollection<TblRecipientChild> TblRecipientChildren { get; } = new List<TblRecipientChild>();
}
