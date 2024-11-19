using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblViewHeader
{
    public int ViewId { get; set; }

    public string ViewDescription { get; set; } = null!;

    public int ViewCreaterUserId { get; set; }

    public string AndOrMode { get; set; } = null!;

    public string? GenderSearch { get; set; }

    public string? GiftTypeSearch { get; set; }

    public string? GiftDescription1Search { get; set; }

    public string? GiftDescription2Search { get; set; }

    public int? AgeFromSearch { get; set; }

    public int? AgeToSearch { get; set; }

    public string? BikeSearch { get; set; }

    public string? GiftCardSearch { get; set; }

    public string? BigItemSearch { get; set; }

    public virtual ICollection<TblViewItemsAgency> TblViewItemsAgencies { get; } = new List<TblViewItemsAgency>();

    public virtual ICollection<TblViewItemsSponsor> TblViewItemsSponsors { get; } = new List<TblViewItemsSponsor>();

    public virtual ICollection<TblViewItemsStatus> TblViewItemsStatuses { get; } = new List<TblViewItemsStatus>();

    public virtual TblUser ViewCreaterUser { get; set; } = null!;
}
