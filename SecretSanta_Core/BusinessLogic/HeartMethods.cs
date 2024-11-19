using SecretSanta_Core.Models;

namespace SecretSanta_Core.BusinessLogic
{
    public class HeartMethods
    {
        public static string GetNameAgeGenderString(AdoptAHeartModel model)
        {
            if (model.AgeType == "months")
            {
                return model.Name + " age: " + model.Age + "m - " + model.Gender;
            }
            return model.Name + " age: " + model.Age + "yrs - " + model.Gender;
        }

        public static string GetWishString(string giftWish, string detail1, string detail2)
        {
            if (String.IsNullOrEmpty(detail1) && String.IsNullOrEmpty(detail2))
            {
                return $"{giftWish}";
            }
            else if (String.IsNullOrEmpty(detail2))
            {
                return $"{giftWish}: {detail1}";
            }

            return $"{giftWish}: {detail1}; {detail2}";
        }
    }
}
