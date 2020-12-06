using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication.Application.Groups.Models;
using WebApplication.Application.Meetings.Models;
using WebApplication.Mongo.Models;

namespace WebApplication.Application.Authorization
{
    public interface IAuthorizationService
    {
        void AuthorizeGroupAccessOrThrow(HttpContext httpContext, string groupId);
        void AuthorizeAccessOrThrow(HttpContext httpContext, string userId);
        void FilterResultByUserRights(HttpContext httpContext, ref List<GroupDto> groups);
        void FilterResultByUserRights(HttpContext httpContext, ref List<MeetingDto> meetings);
    }
}
