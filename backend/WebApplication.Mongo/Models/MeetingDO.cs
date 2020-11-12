﻿using MongoDB.Bson;
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
        public IEnumerable<DateTime> DatePropositions { get; set; }
        public IEnumerable<MovieDo> MoviePropositions { get; set; }
        public IEnumerable<MealDo> MealsPropositions { get; set; }
    }
}