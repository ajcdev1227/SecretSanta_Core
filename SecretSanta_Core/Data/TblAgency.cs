using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblAgency
{
    public int AgencyId { get; set; }

    public string AgencyName { get; set; } = null!;

    public string AgencyCode { get; set; } = null!;

    public string? AgencyStreet { get; set; }

    public string? AgencyCity { get; set; }

    public string? AgencyState { get; set; }

    public string? AgencyZip { get; set; }

    public string? Notes { get; set; }

    public string? Type { get; set; }

    public int? EstimateWishes { get; set; }

    public string? Payment { get; set; }

    public int? Overflow { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public string? Website { get; set; }

    public bool IsActive { get; set; }

    public string? OverflowNotes { get; set; }

    public int NotificationLevel { get; set; }

    public string Region { get; set; } = null!;

    public int? AgencyWebRank { get; set; }

    public virtual TblRegion RegionNavigation { get; set; } = null!;

    public virtual ICollection<TblAgencyLocation> TblAgencyLocations { get; } = new List<TblAgencyLocation>();

    public virtual ICollection<TblRecipientParentArchive> TblRecipientParentArchives { get; } = new List<TblRecipientParentArchive>();

    public virtual ICollection<TblRecipientParent> TblRecipientParents { get; } = new List<TblRecipientParent>();

    public virtual ICollection<TblViewItemsAgency> TblViewItemsAgencies { get; } = new List<TblViewItemsAgency>();
}
