using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo.Models
{
    public class GroupDO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; private set; }
        public string Name { get; set; }
        public IEnumerable<string> MemberIDs { get; set; }
        public IEnumerable<MovieDO> MovieHistory { get; set; }
        public IEnumerable<MealDO> MealsHistory { get; set; }
    }
}
