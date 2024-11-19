using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SecretSanta_Core.Models
{
    public class StoryModel
    {
        [Display(Name="Story #")]
        public int StoryId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "Story")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Story Content is required")]
        public string StoryContent { get; set; }
        public DateTime CreatedDateTime { get; set; }

        [Display(Name="Active")]
        public bool IsActive { get; set; }
    }
}