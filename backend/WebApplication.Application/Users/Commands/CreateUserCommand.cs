using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Authorization;
using WebApplication.Application.Movies.Models;
using WebApplication.Mongo.Models;

namespace WebApplication.Application.Users.Commands
{
    public class CreateUserCommand : IRequest<JWTAuthResult>
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Dictionary<string,double> Movies { get; set; }
        public List<String> Allergens { get; set; }
    }
}
