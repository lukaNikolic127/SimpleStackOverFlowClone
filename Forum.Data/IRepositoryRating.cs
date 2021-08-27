using Forum.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Data
{
    public interface IRepositoryRating : IRepository<Rating>
    {
        public List<Rating> GetAllByCommentId(int id);
        public void DeleteAllByCommentId(int id);
        public int GetRatingUpCountByCommentId(int id);
        public int GetRatingDownCountByCommentId(int id);
        public bool IsCommentRatedUpByMember(int memberId, int commentId);
        public bool IsCommentRatedDownByMember(int memberId, int commentId);
        public Rating GetMembersRatingForComment(int memberId, int commentId);

    }
}
