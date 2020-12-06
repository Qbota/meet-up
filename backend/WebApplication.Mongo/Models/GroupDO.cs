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
        public string Icon { get; set; }
        public string Description { get; set; }
        public string OrganiserID { get; set; }
        public List<string> MemberIDs { get; set; }
        public List<string> MeetingIDs { get; set; }
        public List<MovieDO> MovieHistory { get; set; }
        public List<MealDO> MealsHistory { get; set; }
    }
}
