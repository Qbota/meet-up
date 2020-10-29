using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Application.Movies.Models
{
    public class MovieDto
    {
        public string ID { get; private set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public double Rating { get; set; }
        public MovieGenre Genre { get; set; }
        public IEnumerable<string> Actors { get; set; }

    }

}
