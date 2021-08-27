using System;
using System.Collections.Generic;
using System.Text;

namespace Forum.Data
{
    public interface IRepository<T>
    {
        void Add(T t);
        List<T> GetAll();
        T FindById(int id);
        void Delete(T t);
        void Update(T t);
    }
}
