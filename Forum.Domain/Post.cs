using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Forum.Domain
{
    public class Post
    {
        public int PostId { get; set; }
        [Required]
        [StringLength(256, MinimumLength = 16, ErrorMessage = "Minimum number of characters is 16")]
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public Topic Topic { get; set; }
        public int TopicId { get; set; }
        public Member Member { get; set; }
        public int MemberId { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
