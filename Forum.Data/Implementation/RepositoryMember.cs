using Forum.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forum.Data.Implementation
{
    public class RepositoryMember : IRepositoryMember
    {
        private ForumContext context;
        public RepositoryMember(ForumContext context)
        {
            this.context = context;
        }

        public void Add(Member t)
        {
            context.Members.Add(t);
            //context.SaveChanges();
        }

        public void Delete(Member t)
        {
            context.Members.Remove(t);
            //context.SaveChanges();
        }

        public Member FindById(int id)
        {
            return context.Members.Find(id);
        }

        public List<Member> GetAll()
        {
            return context.Members.ToList();
        }

        public Member GetByUsernameAndPassword(Member member)
        {
            return context.Members.Single(m => m.Username == member.Username && m.Password == member.Password);
        }

        public bool IsUsernameTaken(string username)
        {
            return context.Members.Any(m => m.Username == username);
        }

        public void Update(Member t)
        {
            throw new NotImplementedException();
        }
    }
}
