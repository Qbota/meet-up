using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo.Models
{
    public class MoviePreferenceDO
    {
        public Dictionary<string, double> Ratings { get; set; }
        public List<string> MovieGenres { get; set; }
    }
}
