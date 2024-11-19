using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using SecretSanta_Core.Models;

namespace SecretSanta_Core.Models
{
    public class ReviewModel
    {
        public int LetterId { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Zip")]
        public string Zip { get; set; }

        [Display(Name = "NumChildrenUnder19")]
        public int? NumChildrenUnder19 { get; set; }

        [Display(Name = "NumChildrenOver19")]
        public int? NumChildrenOver19 { get; set; }
        [Display(Name = "NumParents")]
        public int? NumParents { get; set; }

        [Display(Name = "NumGrandparents")]
        public int? NumGrandparents { get; set; }

        [Display(Name = "NumOtherFamily")]
        public int? NumOtherFamily { get; set; }

        [Display(Name = "LetterLabel")]
        public string Letter { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Display(Name = "Writer Name")]
        public string WriterName { get; set; }

        [Required(ErrorMessage = "Required")]
        [Display(Name = "Phone")] 
        public string Phone { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "InvalidEmail")]
        public string Email { get; set; }

        [Display(Name = "Agency")]
        public string Agency { get; set; }

        public FamilyMemberModel[] FamilyMembers { get; set; }


    }

}