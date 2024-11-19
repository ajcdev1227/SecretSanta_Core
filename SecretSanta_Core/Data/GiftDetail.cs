using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class GiftDetail
{
    public int GiftDetailId { get; set; }

    public string? GiftIdeaDescription { get; set; }

    public string? LblGiftDetail1 { get; set; }

    public string? LblGiftDetail2 { get; set; }

    public int GiftDetailOrder { get; set; }

    public string GiftDetailText { get; set; } = null!;

    public virtual ICollection<TblRecipientParent> TblRecipientParentAltGiftTypeNavigations { get; } = new List<TblRecipientParent>();

    public virtual ICollection<TblRecipientParentArchive> TblRecipientParentArchiveAltGiftTypeNavigations { get; } = new List<TblRecipientParentArchive>();

    public virtual ICollection<TblRecipientParentArchive> TblRecipientParentArchiveGiftTypeNavigations { get; } = new List<TblRecipientParentArchive>();

    public virtual ICollection<TblRecipientParent> TblRecipientParentGiftTypeNavigations { get; } = new List<TblRecipientParent>();
}
