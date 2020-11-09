using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Mongo.Models;

namespace WebApplication.Application.Authorization
{
    public interface IAuthorizationService
    {
        bool AuthorizeGroupAccessAsync(UserDO user, string groupId);
        bool AuthorizeAccessAsync(UserDO user, string userId);
    }
}
