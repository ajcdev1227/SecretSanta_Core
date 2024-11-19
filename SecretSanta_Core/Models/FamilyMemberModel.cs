using System.ComponentModel.DataAnnotations;

namespace SecretSanta_Core.Models
{
    public class FamilyMemberModel
    {
        public int FamilyMemberId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Age")]
        public int? Age { get; set; }

        [Display(Name = "AgeType")]
        public string AgeType { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Warm Clothing Size")]
        [Required(ErrorMessage = "Required")]
        public string WarmClothingSize { get; set; }

        [Required(ErrorMessage = "Required")]
        public string WarmClothingType { get; set; }

        [Display(Name = "Shoe Size")]
        [Required(ErrorMessage = "Required")]
        public string ShoeSize { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Interest / Hobbies / Likes")]
        public string Likes { get; set; }

        [Display(Name = "Other Requests")]
        public string OtherRequests { get; set; }

        [Required(ErrorMessage = "Required")]
        public string ShoeSizeType { get; set; }

        public int LetterId { get; set; }

        public string InfoString { get; set; }
    }
}