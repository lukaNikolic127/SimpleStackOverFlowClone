using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Forum.Domain
{
    public class Comment
    {
        public int CommentId { get; set; }
        [Required]
        [StringLength(256, MinimumLength = 16, ErrorMessage = "Minimum number of characters is 16")]
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public Member Member { get; set; }
        public int MemberId { get; set; }
        public Post Post { get; set; }
        public int PostId { get; set; }
        public List<Rating> Ratings { get; set; }
        public int RatingUps { get; set; }
    }
}
