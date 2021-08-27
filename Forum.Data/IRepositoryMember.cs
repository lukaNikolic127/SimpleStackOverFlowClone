using Forum.Domain;
using System;
using System.Collections.Generic;

namespace Forum.Data
{
    public interface IRepositoryMember : IRepository<Member>
    {
        public Member GetByUsernameAndPassword(Member member);
        public bool IsUsernameTaken(string username);
    }

}
