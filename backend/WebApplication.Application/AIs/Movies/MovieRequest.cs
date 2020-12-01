using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.AIs.Movies
{
    public class MovieRequest
    {
        public Dictionary<string, int> Genres { get; set; }
        public List<Dictionary<string, double>> Ratings { get; set; }
    }
}
