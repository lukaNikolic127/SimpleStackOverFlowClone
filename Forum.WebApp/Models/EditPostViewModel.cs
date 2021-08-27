using Forum.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.WebApp.Models
{
    public class EditPostViewModel
    {
        public Post Post { get; set; }
        public string Direct { get; set; }
        public List<SelectListItem> Topics { get; set; }
    }
}
