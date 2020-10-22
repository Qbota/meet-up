using System.Collections.Generic;
using WebApplication.Mock.Entity;

namespace WebApplication.Mock.Repository
{
    public class MockInMemoryRepository : IRepository<MockEntity>
    {
        private static readonly List<MockEntity> Entities= new List<MockEntity>()
        {
            new MockEntity("1","hi"),
            new MockEntity("2","hi"),
            new MockEntity("3","hi")
        };
        
        public IEnumerable<MockEntity> GetAll()
        {
            return Entities;
        }

        public void Insert(MockEntity entity)
        {
            Entities.Add(entity);
        }
    }
}