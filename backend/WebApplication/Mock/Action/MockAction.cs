using System;
using WebApplication.Mock.Entity;
using WebApplication.Mock.Repository;

namespace WebApplication.Mock.Action
{
    public class MockAction : IAction
    {
        private readonly IRepository<MockEntity> _repository = new MockInMemoryRepository();
        private readonly MockEntity _entity;
        
        public MockAction(MockEntity entity)
        {
            _entity = entity;
        }
        
        public void Execute()
        {
            if (_entity == null)
            {
                throw new NullReferenceException();
            }
            _repository.Insert(_entity);
        }
        
    }
}