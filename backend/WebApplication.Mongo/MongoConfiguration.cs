using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo
{
    public class MongoConfiguration : IOptions<MongoConfiguration>
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public MongoConfiguration Value => this;
    }
}
