using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo.Models
{
    public class MovieDO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id;
        public string ID { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }
        public List<string> Genres { get; set; }
        public string Poster { get; set; }
        public string Date { get; set; }
        public bool IsBasicSet { get; set; }
    }
}
