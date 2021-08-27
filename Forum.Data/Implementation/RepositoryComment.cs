using Forum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forum.Data.Implementation
{
    public class RepositoryComment : IRepositoryComment
    {
        private ForumContext context;

        public RepositoryComment(ForumContext context)
        {
            this.context = context;
        }

        public void Add(Comment t)
        {
            context.Comments.Add(t);
        }

        public void Delete(Comment t)
        {
            context.Comments.Remove(t);
        }

        public void DeleteAllByPostId(int id)
        {
            List<Comment> comments = context.Comments.Where(c => c.PostId == id).ToList();
            foreach (var comment in comments)
            {
                context.Comments.Remove(comment);
            }
        }

        public Comment FindById(int id)
        {
            return context.Comments.Find(id);
        }

        public List<Comment> GetAll()
        {
            List<Comment> comments = context.Comments.ToList();
            return comments.OrderByDescending(c => c.RatingUps).ToList();
        }

        public List<Comment> GetAllByPostId(int id)
        {
            List<Comment> comments = context.Comments.Where(c => c.PostId == id).ToList();
            return comments.OrderByDescending(c => c.RatingUps).ToList();
        }

        public void RateDown(int id)
        {
            Comment comment = context.Comments.Find(id);
            comment.RatingUps--;
        }

        public void RateUp(int id)
        {
            Comment comment = context.Comments.Find(id);
            comment.RatingUps++;
        }

        public void Update(Comment t)
        {
            throw new NotImplementedException();
        }
    }
}
