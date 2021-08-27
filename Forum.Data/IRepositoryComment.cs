using Forum.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Data
{
    public interface IRepositoryComment : IRepository<Comment>
    {
        public void DeleteAllByPostId(int id);
        public List<Comment> GetAllByPostId(int id);
        public void RateUp(int id);
        public void RateDown(int id);

    }
}
