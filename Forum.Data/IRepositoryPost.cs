using Forum.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Data
{
    public interface IRepositoryPost : IRepository<Post>
    {
        public List<Post> GetAllByMember(int memberId);
        public List<Post> GetAllByTopic(int topicId);

    }
}
