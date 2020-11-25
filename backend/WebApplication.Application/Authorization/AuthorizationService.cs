using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication.Application.Exceptions;
using WebApplication.Application.Groups.Models;
using WebApplication.Application.Meetings.Models;
using WebApplication.Mongo.Models;

namespace WebApplication.Application.Authorization
{
    public class AuthorizationService : IAuthorizationService
    {
        public void AuthorizeGroupAccessOrThrow(HttpContext httpContext, string groupId)
        {
            var user = httpContext.Items["Account"] as UserDO;
            if (user == null || !user.GroupIDs.Contains(groupId))
            {
                throw new AuthorizationException("No access to this group");
            }
        }
        public void AuthorizeAccessOrThrow(HttpContext httpContext, string userId)
        {
            var user = httpContext.Items["Account"] as UserDO;
            if (user == null || !String.Equals(user.ID, userId))
            {
                throw new AuthorizationException("No access to this user");
            } 
        }

        public void FilterResultByUserRights(HttpContext httpContext, ref List<GroupDto> groups)
        {
            var user = httpContext.Items["Account"] as UserDO;
            if (user is null)
            {
                throw new AuthorizationException("No access to groups");
            }
            groups = groups.Where(x => user.GroupIDs.Contains(x.ID)).ToList();
        }

        public void AuthorizeMeetingAccessOrThrow(HttpContext httpContext, string meetingId)
        {
            var user = httpContext.Items["Account"] as UserDO;
            if (user == null || !user.MeetingIDs.Contains(meetingId))
            {
                throw new AuthorizationException("No access to this meeting");
            }
        }

        public void FilterResultByUserRights(HttpContext httpContext, ref List<MeetingDto> meetings)
        {
            var user = httpContext.Items["Account"] as UserDO;
            if (user is null)
            {
                throw new AuthorizationException("No access to groups");
            }
            meetings = meetings?.Where(x => user.MeetingIDs.Contains(x.ID))?.ToList();
        }
    }
}
