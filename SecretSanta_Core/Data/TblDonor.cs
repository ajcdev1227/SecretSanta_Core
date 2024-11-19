using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblDonor
{
    public string DonorId { get; set; } = null!;

    public string DonorEmail { get; set; } = null!;

    public virtual ICollection<TblDonorThankYou> TblDonorThankYous { get; } = new List<TblDonorThankYou>();

    public virtual ICollection<TblRecipientParentArchive> TblRecipientParentArchives { get; } = new List<TblRecipientParentArchive>();

    public virtual ICollection<TblRecipientParent> TblRecipientParents { get; } = new List<TblRecipientParent>();
}
