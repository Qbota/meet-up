using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Groups.Models;

namespace WebApplication.Application.Groups.Queries
{
    
    public class GetGroupQuery : IRequest<GroupDto>
    {
        public string Id { get; set; }
    }
}
