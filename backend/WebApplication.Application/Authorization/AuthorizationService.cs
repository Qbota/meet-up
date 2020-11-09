using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication.Mongo.Models;

namespace WebApplication.Application.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        public bool AuthorizeGroupAccessAsync(UserDO user, string groupId)
        {
            if (user.GroupIDs.Contains(groupId))
                return true;
            return false;
        }
        public bool AuthorizeAccessAsync(UserDO user, string userId)
        {
            if (String.Equals(user.ID,userId))
                return true;
            return false;
        }
    }
}
