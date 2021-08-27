using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.WebApp.Models
{
    public class RateViewModel
    {
        public int CommentId { get; set; }
        public bool RatedUp { get; set; }
        public int RatingUp { get; set; }
        public bool RatedDown { get; set; }
        public int RatingDown { get; set; }
    }
}
