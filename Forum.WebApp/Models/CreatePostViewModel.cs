using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.WebApp.Models
{
    public class CreatePostViewModel
    {
        public List<SelectListItem> Topics { get; set; }
        public int TopicId { get; set; }
        public int MemberId { get; set; }
        [Required]
        [StringLength(256, MinimumLength = 16, ErrorMessage = "Minimum number of characters is 16")]
        public string Content { get; set; }
        public DateTime DateTime { get; set; }

    }
}
