using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblSponsor
{
    public int SponsorId { get; set; }

    public string SponsorName { get; set; } = null!;

    public string? SponsorStreet { get; set; }

    public string? SponsorCity { get; set; }

    public string? SponsorState { get; set; }

    public string? SponsorZip { get; set; }

    public string? SS_FirstName { get; set; }

    public string? SS_LastName { get; set; }

    public string? SS_Phone { get; set; }

    public string? SS_AltPhone { get; set; }

    public string? PrimaryFirstName { get; set; }

    public string? PrimaryLastName { get; set; }

    public string? PrimaryPhone { get; set; }

    public string? PrimaryEmail { get; set; }

    public string? Notes { get; set; }

    public int? ElfID { get; set; }

    public string? SS_Email { get; set; }

    public string? DisplayType { get; set; }

    public string? DisplaySignage { get; set; }

    public string? DeliveryDate { get; set; }

    public string? SongRequest { get; set; }

    public bool CommunitySponsor { get; set; }

    public byte[] Row_Version { get; set; } = null!;

    public bool AdoptAHeartDisplay { get; set; }

    public bool IsActive { get; set; }

    public string? LYGiftAssigned { get; set; }

    public string? LYGiftOut { get; set; }

    public string Region { get; set; } = null!;

    public bool OpenToPublic { get; set; }

    public virtual TblElf? Elf { get; set; }

    public virtual TblRegion RegionNavigation { get; set; } = null!;

    public virtual ICollection<TblRecipientParentArchive> TblRecipientParentArchives { get; } = new List<TblRecipientParentArchive>();

    public virtual ICollection<TblViewItemsSponsor> TblViewItemsSponsors { get; } = new List<TblViewItemsSponsor>();
}
