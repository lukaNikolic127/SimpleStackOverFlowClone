using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepositoryMember Member { get; set; }
        public IRepositoryTopic Topic { get; set; }
        public IRepositoryPost Post { get; set; }
        public IRepositoryComment Comment { get; set; }
        public IRepositoryRating Rating { get; set; }
        public void Commit();
    }
}
