using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblRecipientParentArchive
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

    public int? SponsorId { get; set; }

    public bool Resubmit { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public string? EditNotes { get; set; }

    public string? Location { get; set; }

    public string Year { get; set; } = null!;

    public bool? IsActive { get; set; }

    public string? DonorId { get; set; }

    public DateTime? DonorRegisterDate { get; set; }

    public DateTime? DateEntered { get; set; }

    public string? Freefield { get; set; }

    public int? RecGiftCountGiftInOut { get; set; }

    public bool? ThankYouRecieved { get; set; }

    public int? WebRank { get; set; }

    public virtual TblAgency? Agency { get; set; }

    public virtual GiftDetail? AltGiftTypeNavigation { get; set; }

    public virtual TblDonor? Donor { get; set; }

    public virtual GiftDetail? GiftTypeNavigation { get; set; }

    public virtual TblSponsor? Sponsor { get; set; }

    public virtual ICollection<TblRecipientChildArchive> TblRecipientChildArchives { get; } = new List<TblRecipientChildArchive>();
}
