using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo.Models
{
    public class UserDO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get;  set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get;  set; }
        public byte[] Salt { get; set; }
        public List<DateTime> AvailableDates { get; set; }
        public List<string> GroupIDs { get; set; }
        public List<string> MeetingIDs { get; set; }
        public MoviePreferenceDO MoviePreference { get; set; }
        public MealPreferenceDO MealPreference { get; set; }
    }
}
