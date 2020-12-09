using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.AIs;
using WebApplication.Application.Authorization;
using WebApplication.Application.Exceptions;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Recomendations.Queries
{
    public class GetDateRecomendationQueryHandler : IRequestHandler<GetDateRecomendationQuery, DateTime>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDatePickerService _datePickerService;
        public GetDateRecomendationQueryHandler(
            IDatePickerService datePickerService,
            IMeetingRepository meetingRepository,
            IUserRepository userRepository,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _meetingRepository = meetingRepository;
            _userRepository = userRepository;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _datePickerService = datePickerService;
        }
        public async Task<DateTime> Handle(GetDateRecomendationQuery request, CancellationToken cancellationToken)
        {
            var meeting = await _meetingRepository.GetMeetingByIdAsync(request.MeetingID);
            _authorizationService.AuthorizeGroupAccessOrThrow(_httpContextAccessor.HttpContext, meeting.GroupID);
            if (meeting is null)
                throw new NotFoundException();
            var users = await _userRepository.GetUsersByGroupIdAsync(meeting.GroupID);
            return _datePickerService.PickDate(GetDates(users));
        }

        private List<DateTime> GetDates(IEnumerable<UserDO> users)
        {
            List<DateTime> dates = new List<DateTime>();
            foreach (var user in users)
            {
                dates.AddRange(user.AvailableDates.Where(x => x > DateTime.Now));
            }
            return dates;
        }
    }
}
