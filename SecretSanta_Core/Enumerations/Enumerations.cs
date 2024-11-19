namespace SecretSanta_Core.Enumerations
{
	public enum StatusTypes
	{
		Draft = 10,
		New = 20,
		Approved = 30,
		Edit = 32,
		Edited = 34,
		CardPrint = 40,
		HeartAssign = 50,
		HeartReassign = 52,
		HomeAvailable = 55,
		HomeAdopt = 57,
		FundAHeart = 58,
		HeartRegistered = 59,
		GiftIn = 60,
		GiftInGiftCard = 62,
		GiftInBike = 64,
		LabelReprint = 70,
		NewLabel = 72,
		HomeLabel = 75,
		GiftOut = 80,
		GiftOutGiftCard = 81,
		GiftOutBike = 82,
		CancelRecord = 90,
		Other = 96,
		LetterHomePost = 110,
		LetterHomeAdopt = 115
	}
	public enum DashboardTypes
	{
		Draft,
		New,
		Approved,
		Revise,
		Active,
		Cancel,
		WebAvail,
		WebAdopt,
		In,
		InBike,
		InGifCard,
		Out,
		OutBike,
		OutGfCard,
		Other,
		Pending,
	}

	public enum PieChartGroup
	{
		InProces,
		GiftIn,
		GiftOut
	}

	public enum BarChartGroup
	{
		GiftIn,
		GiftOut
	}
}
