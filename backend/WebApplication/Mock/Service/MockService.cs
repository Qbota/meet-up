using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WebApplication.Mock.Action;
using WebApplication.Mock.Entity;
using WebApplication.Mock.Repository;

namespace WebApplication.Mock.Service
{
    public class MockService : IMockService
    {
        private readonly IRepository<MockEntity> _repository = new MockInMemoryRepository();
        private readonly GenericActionProcessor _processor = new GenericActionProcessor(new Logger<GenericActionProcessor>(new LoggerFactory()));

        public IEnumerable<MockEntity> GetAllMockEntities() => _repository.GetAll();

        public MockEntity InsertMockEntity(MockEntity mockEntity)
        {
            _processor.Process(new MockAction(mockEntity));
            return mockEntity;
        }
    }
}