using Forum.Domain.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Forum.Domain
{
    public class Topic
    {
        public int TopicId { get; set; }
        [Required(ErrorMessage = "Topic name is required")]
        //[StringLength(10)] // max
        //[MinLength(3)]
        [TopicName(ErrorMessage = "Topic name must have at least 3 characters")]
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
    }
}
