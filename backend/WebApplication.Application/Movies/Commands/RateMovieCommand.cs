using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.Application.Movies.Commands
{
    public class RateMovieCommand : IRequest<string>
    {
        public string UserID { get; set; }
        public string MovieID { get; set; }
        public double Rating { get; set; }
    }
}
