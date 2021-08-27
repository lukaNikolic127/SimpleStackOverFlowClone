using Forum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Forum.Data.Implementation
{
    public class RepositoryTopic : IRepositoryTopic
    {
        private ForumContext context;

        public RepositoryTopic(ForumContext context)
        {
            this.context = context;
        }

        public void Add(Topic t)
        {
            context.Topics.Add(t);
        }

        public void Delete(Topic t)
        {
            
            context.Topics.Remove(t);
        }

        public Topic FindById(int id)
        {
            return context.Topics.Find(id);
        }

        public List<Topic> GetAll()
        {
            return context.Topics.ToList();
        }

        public List<Topic> Search(Expression<Func<Topic, bool>> p)
        {
            return context.Topics.Where(p).ToList();
        }

        public void Update(Topic t)
        {
            throw new NotImplementedException();
        }
    }
}
