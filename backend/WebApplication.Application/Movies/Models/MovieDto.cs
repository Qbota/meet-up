using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;

namespace WebApplication.Application.Movies.Models
{
    public class MovieDto
    {
        public string ID { get; private set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public double Rating { get; set; }
        public List<string> Genres { get; set; }
        public string Date { get; set; }
        public string Poster { get; set; }
    }

}
