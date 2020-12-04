using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Meetings.Commands
{
    public class DeleteMeetingCommandHandler : IRequestHandler<DeleteMeetingCommand, string>
    {
        private readonly IMeetingRepository _meetingRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeleteMeetingCommandHandler(
            IMeetingRepository meetingRepository,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _meetingRepository = meetingRepository;
            _httpContextAccessor = httpContextAccessor;
            _authorizationService = authorizationService;
        }
        public async Task<string> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeMeetingAccessOrThrow(_httpContextAccessor.HttpContext, request.Id);
            await _meetingRepository.DeleteMeetingAsync(request.Id);
            return request.Id;
        }
    }
}
