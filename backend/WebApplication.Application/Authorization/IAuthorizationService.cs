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
        void AuthorizeGroupAccessOrThrowAsync(HttpContext httpContext, string groupId);
        void AuthorizeAccessOrThrowAsync(HttpContext httpContext, string userId);
        void FilterResultByUserRightsAsync(HttpContext httpContext, ref List<GroupDto> groups);
        void AuthorizeMeetingAccessOrThrowAsync(HttpContext httpContext, string meetingId);
        void FilterResultByUserRightsAsync(HttpContext httpContext, ref List<MeetingDto> meetings);
    }
}
