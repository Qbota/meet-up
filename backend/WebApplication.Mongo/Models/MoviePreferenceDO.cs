using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Mongo.Models
{
    public class MoviePreferenceDO
    {
        public IEnumerable<string> Actors { get; set; }
        public IEnumerable<string> Directors { get; set; }
        public IEnumerable<MovieDo> Movies { get; set; }
        public IEnumerable<MovieGenre> MovieGenres { get; set; }
    }
}
