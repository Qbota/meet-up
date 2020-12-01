using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Mongo.Models;

namespace WebApplication.Application.Movies.Models
{
    public class MoviePreferenceDto
    {
        public MovieDto Movies { get; set; }
        public Dictionary<string, double> Ratings { get; set; }
        public IEnumerable<string> MovieGenres { get; set; }
    }
}
