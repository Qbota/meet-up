using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo.Models
{
    public class MeetingDO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; private set; }
        public string Title { get; set; }
        public string GroupID { get; set; }
        public string OrganiserID { get; set; }
        public string Description { get; set; }
        public List<DateTime> DatePropositions { get; set; }
        public List<MovieDO> MoviePropositions { get; set; }
        public List<MealDO> MealsPropositions { get; set; }
    }
}
