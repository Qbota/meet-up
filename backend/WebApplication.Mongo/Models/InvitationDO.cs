using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo.Models
{
    public class InvitationDO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string SenderName { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public string UserId { get; set; }
    }
}
