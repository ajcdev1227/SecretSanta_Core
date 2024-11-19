using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblLetterSantaArchive
{
    public int LetterId { get; set; }

    public string Language { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public string? Agency { get; set; }

    public bool? SingleMom { get; set; }

    public bool? SingleDad { get; set; }

    public bool? Over65 { get; set; }

    public bool? Under19 { get; set; }

    public bool? Disabled { get; set; }

    public bool? Veteran { get; set; }

    public bool? Parent { get; set; }

    public bool? Grandparent { get; set; }

    public bool? FullTime { get; set; }

    public bool? PartTime { get; set; }

    public bool? Calworks { get; set; }

    public bool? CalFresh { get; set; }

    public bool? Disability { get; set; }

    public bool? SocialSecurity { get; set; }

    public bool? Unemployment { get; set; }

    public bool? OtherEmployment { get; set; }

    public bool? KZST { get; set; }

    public bool? VolunteerCenter { get; set; }

    public bool? twooneone { get; set; }

    public bool? NonProfit { get; set; }

    public bool? FriendFamily { get; set; }

    public bool? Other { get; set; }

    public string? OtherHear { get; set; }

    public int? NumChildrenUnder19 { get; set; }

    public int? NumChildrenOver19 { get; set; }

    public int? NumParents { get; set; }

    public int? NumGrandparents { get; set; }

    public int? NumOtherFamily { get; set; }

    public string Letter { get; set; } = null!;

    public string? FirstNeedTypeId { get; set; }

    public string? SecondNeedTypeId { get; set; }

    public string? ThirdNeedTypeId { get; set; }

    public bool? DifferentLetter { get; set; }

    public bool? HouseholdLetter { get; set; }

    public bool? OutsideHouseholdLetter { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? WriterName { get; set; }

    public int NumFriends { get; set; }

    public string? ForWho { get; set; }

    public string? FosterYouth { get; set; }

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? AdoptedBy { get; set; }

    public string? AdoptedByPhone { get; set; }

    public string? AdoptedByEmail { get; set; }

    public string? LetterSummary { get; set; }

    public string? SantaLetterCode { get; set; }

    public string Year { get; set; }

    public string? FamilyCode { get; set; }

    public bool? IsActive { get; set; }

    public string? AdminGeneralNotes { get; set; }

    public string? AdminHistoryNotes { get; set; }

    public string? AdminGeneralNotes1 { get; set; }

    public DateTime? LetterReadyDate { get; set; }

    public DateTime? PickupDate { get; set; }

    public virtual ICollection<TblLetterStatusArchive> TblLetterStatusArchives { get; } = new List<TblLetterStatusArchive>();
}
