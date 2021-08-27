using Forum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forum.Data.Implementation
{
    public class RepositoryRating : IRepositoryRating
    {
        private ForumContext context;
        public RepositoryRating(ForumContext context)
        {
            this.context = context;
        }
        public void Add(Rating t)
        {
            context.Ratings.Add(t);
        }

        public void Delete(Rating t)
        {
            context.Ratings.Remove(t);
        }

        public Rating FindById(int id)
        {
            return context.Ratings.Find(id);
        }

        public List<Rating> GetAll()
        {
            return context.Ratings.ToList();
        }

        public List<Rating> GetAllByCommentId(int id)
        {
            return context.Ratings.Where(r => r.CommentId == id).ToList();
        }

        public void DeleteAllByCommentId(int id)
        {
            List<Rating> ratings = context.Ratings.Where(r => r.CommentId == id).ToList();
            foreach (var rating in ratings)
            {
                context.Ratings.Remove(rating);
            }
        }

        public void Update(Rating t)
        {
            throw new NotImplementedException();
        }

        public int GetRatingUpCountByCommentId(int id)
        {
            return context.Ratings.Where(r => r.CommentId == id && r.Like == true).ToList().Count();
        }

        public int GetRatingDownCountByCommentId(int id)
        {
            return context.Ratings.Where(r => r.CommentId == id && r.Dislike == true).ToList().Count();
        }

        public bool IsCommentRatedUpByMember(int memberId, int commentId)
        {
            return context.Ratings.Any(r => r.MemberId == memberId && r.CommentId == commentId && r.Like == true);
        }

        public bool IsCommentRatedDownByMember(int memberId, int commentId)
        {
            return context.Ratings.Any(r => r.MemberId == memberId && r.CommentId == commentId && r.Dislike == true);
        }

        public Rating GetMembersRatingForComment(int memberId, int commentId)
        {
            return context.Ratings.SingleOrDefault(r => r.MemberId == memberId && r.CommentId == commentId);
        }
    }
}
