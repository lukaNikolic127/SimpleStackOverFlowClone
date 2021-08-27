using Forum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forum.Data.Implementation
{
    public class RepositoryPost : IRepositoryPost
    {
        private ForumContext context;

        public RepositoryPost(ForumContext context)
        {
            this.context = context;
        }

        public void Add(Post t)
        {
            context.Posts.Add(t);
        }

        public void Delete(Post t)
        {
            context.Posts.Remove(t);
        }

        public Post FindById(int id)
        {
            return context.Posts.Find(id);
        }

        public List<Post> GetAll()
        {
            List<Post> posts = context.Posts.ToList();
            return posts.OrderByDescending(p => p.DateTime).ToList();
        }

        public List<Post> GetAllByMember(int memberId)
        {
            List<Post> posts = context.Posts.Where(p => p.MemberId == memberId).ToList();
            return posts.OrderByDescending(p => p.DateTime).ToList();
        }

        public List<Post> GetAllByTopic(int topicId)
        {
            List<Post> posts = context.Posts.Where(p => p.TopicId == topicId).ToList();
            return posts.OrderByDescending(p => p.DateTime).ToList();
        }

        public void Update(Post t)
        {
            Post post = context.Posts.Find(t.PostId);
            post.Content = t.Content;
            post.DateTime = DateTime.Now;
        }
    }
}
