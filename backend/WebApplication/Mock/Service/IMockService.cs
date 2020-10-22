using System.Collections.Generic;
using WebApplication.Mock.Entity;

namespace WebApplication.Mock.Service
{
    public interface IMockService
    {
        IEnumerable<MockEntity> GetAllMockEntities();
        MockEntity InsertMockEntity(MockEntity entity);
    }
}