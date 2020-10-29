using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.Movies.Models
{
    public class MoviePreferenceDto
    {
        public IEnumerable<string> Actors { get; set; }
        public IEnumerable<string> Directors { get; set; }
        public IEnumerable<MovieDto> Movies { get; set; }
        public IEnumerable<MovieGenre> MovieGenres { get; set; }
        
    }
}
