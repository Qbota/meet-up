using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo.Models
{
    public class MovieDO
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public double Rating { get; set; }
        public List<string> Genres { get; set; }
        public string Poster { get; set; }
        public string Date { get; set; }
        public bool IsBasicSet { get; set; }
    }
}
