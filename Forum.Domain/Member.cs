using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.Domain
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Rating> Ratings { get; set; }

    }
}
