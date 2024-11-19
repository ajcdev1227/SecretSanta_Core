using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SecretSanta_Core.Data;

public partial class TblFamilyMember
{
    [JsonProperty("familyMemberId")]
    public int FamilyMemberId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("age")]
    public int Age { get; set; }

    [JsonProperty("agetype")]
    public string AgeType { get; set; }

    [JsonProperty("gender")]
    public string? Gender { get; set; }

    [JsonProperty("warmClothingSize")]
    public string WarmClothingSize { get; set; } = null!;

    [JsonProperty("shoeSize")]
    public string ShoeSize { get; set; } = null!;

    [JsonProperty("likes")] 
    public string Likes { get; set; } = null!;

    [JsonProperty("otherRequests")]
    public string? OtherRequests { get; set; }

    public int LetterId { get; set; }

    [JsonProperty("warmClothingType")] 
    public string WarmClothingType { get; set; } = null!;

    [JsonProperty("shoeSizeType")] 
    public string ShoeSizeType { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual TblLetterSanta Letter { get; set; } = null!;
}
