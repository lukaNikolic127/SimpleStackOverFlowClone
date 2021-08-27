using Forum.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Forum.Data
{
    public interface IRepositoryTopic : IRepository<Topic>
    {
        List<Topic> Search(Expression<Func<Topic, bool>> p);
    }
}
