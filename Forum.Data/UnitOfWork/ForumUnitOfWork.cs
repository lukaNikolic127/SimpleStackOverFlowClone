using Forum.Data.Implementation;
using Forum.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Data.UnitOfWork
{
    public class ForumUnitOfWork : IUnitOfWork
    {
        private ForumContext context;
        public ForumUnitOfWork(ForumContext context)
        {
            this.context = context;
            Member = new RepositoryMember(context);
            Topic = new RepositoryTopic(context);
            Post = new RepositoryPost(context);
            Comment = new RepositoryComment(context);
            Rating = new RepositoryRating(context);
        }
        public IRepositoryMember Member { get; set; }
        public IRepositoryTopic Topic { get; set; }
        public IRepositoryPost Post { get; set; }
        public IRepositoryComment Comment { get; set; }
        public IRepositoryRating Rating { get; set; }


        public void Commit()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
