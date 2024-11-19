using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SecretSanta_Core.Data;

public partial class TblHeartArchive
{
    public int? RecipientNum { get; set; }

    public int? Age { get; set; }

    public string? AgeType { get; set; }

    public string? Gender { get; set; }

    public int? Months { get; set; }

    public int? GiftType { get; set; }

    public int? AltGiftType { get; set; }

    public int? AgencyID { get; set; }

    public bool? HeartIsActive { get; set; }

    public string? DonorId { get; set;}

    public bool? ThankYouRecieved { get; set; }

    public int? LastSponsorID {  get; set; }

    public string? Year { get; set; }

    public string? GiftCountInOut {  get; set; }

    public int? FloorGiftCount {  get; set; }

    public int? GiftCardCount {  get; set; }

    public int? BikeCount {  get; set; }

    public int? TotalGiftCount { get; set; }

}
