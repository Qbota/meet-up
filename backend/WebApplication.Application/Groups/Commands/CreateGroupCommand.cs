using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Meals.Models;
using WebApplication.Application.Movies.Models;

namespace WebApplication.Application.Groups.Commands
{
    public class CreateGroupCommand : IRequest<string>
    {
        public string Name { get; set; }
        public List<string> MemberIDs { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
