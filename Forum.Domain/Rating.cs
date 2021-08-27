using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Domain
{
    public class Rating
    {
        public int RatingId { get; set; }
        public bool Like { get; set; }
        public bool Dislike { get; set; }
        public DateTime DateTime { get; set; }
        public Member Member { get; set; }
        public int MemberId { get; set; }
        public Comment Comment { get; set; }
        public int CommentId { get; set; }
    }
}
