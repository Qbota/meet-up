using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo
{
    public class MongoConfiguration : IOptions<MongoConfiguration>
    {
        public string UserCollectionName { get; set; }
        public string GroupCollectionName { get; set; }
        public string MeetingCollectionName { get; set; }
        public string MovieCollectionName { get; set; }
        public string InvitationCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        public MongoConfiguration Value => this;
    }
}
