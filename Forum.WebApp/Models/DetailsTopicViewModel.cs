using Forum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.WebApp.Models
{
    public class DetailsTopicViewModel
    {
        public Topic Topic { get; set; }
        public List<Post> Posts { get; set; }
    }
}
