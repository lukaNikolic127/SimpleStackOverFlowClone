using Forum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.WebApp.Models
{
    public class CreateCommentViewModel
    {
        public Post Post { get; set; }
        public string PostMemberUsername { get; set; }
        public DateTime PostDateTime { get; set; }
        public string PostContent { get; set; }
        public Comment Comment { get; set; }
        public int MemberId { get; set; }
    }
}
