using System;
using System.Collections.Generic;

namespace WebApplication.Mock.Repository
{
    public interface IRepository<T>
    {
        //Add here crud operations
        IEnumerable<T> GetAll();
        void Insert(T entity);
    }
}