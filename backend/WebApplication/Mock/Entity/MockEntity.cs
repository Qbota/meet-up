using System;

namespace WebApplication.Mock.Entity
{
    public class MockEntity
    {
        public string Id { get; set; }
        public string Field { get; set; }

        public MockEntity(string id, string field)
        {
            Id = id;
            Field = field;
        }

        public MockEntity()
        {
            //Should be empty
        }
    }
}