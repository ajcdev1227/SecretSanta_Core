using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblLetterSanta
{
    public int LetterId { get; set; }

    public string Address { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public string? Agency { get; set; }

    public int? NumChildrenUnder19 { get; set; }

    public int? NumChildrenOver19 { get; set; }

    public int? NumParents { get; set; }

    public int? NumGrandparents { get; set; }

    public int? NumOtherFamily { get; set; }

    public string Letter { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? WriterName { get; set; } 

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FamilyCode { get; set; }

    public string? AdminGeneralNotes { get; set; }

    public string? AdoptedBy { get; set; }

    public string? AdoptedByPhone { get; set; }

    public string? AdoptedByEmail { get; set; }

    public string? AdminHistoryNotes { get; set; }

    public string? LetterSummary { get; set; }

    public string? AdminGeneralNotes1 { get; set; }

    public bool IsActive { get; set; }

    public DateTime? LetterReadyDate { get; set; }

    public DateTime? PickupDate { get; set; }

    public virtual ICollection<TblFamilyMember> TblFamilyMembers { get; } = new List<TblFamilyMember>();

    public virtual ICollection<TblLetterStatus> TblLetterStatuses { get; } = new List<TblLetterStatus>();
}
