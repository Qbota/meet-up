using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Application.Movies.Models
{
    public class Movie
    {
        public string ID { get; private set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int Rating { get; set; }
        public MovieGenre Genre { get; set; }
        
        public IEnumerable<string> Actors { get; set; }

        public void SetID(string id)
        {
            this.ID = id;
        }
    }
    public enum MovieGenre
    {
        Comedy,
        Horror,
        Action,
        Adventure,
        Crime,
        Romance,
        Drama,
        Fantasy,
        Historical,
        ScienceFiction,
        Thiller,
        Western


    }
}
