using System;
using System.Collections.Generic;

namespace SecretSanta_Core.Data;

public partial class TblAgencyContact
{
    public string Id { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? AltPhone { get; set; }

    public string? Fax { get; set; }

    public int? EstimateWishes { get; set; }

    public virtual ICollection<TblMessageContact> TblMessageContacts { get; } = new List<TblMessageContact>();
}
