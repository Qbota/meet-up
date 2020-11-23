using System.Collections.Generic;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Mongo.Repositories;
using WebApplication.Application.Invitations.Models;

namespace WebApplication.Application.Invitations.Queries
{
    public class GetUserInvitationsQueryHandler : IRequestHandler<GetUserInvitationsQuery, IEnumerable<InvitationDto>>
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetUserInvitationsQueryHandler(
            IInvitationRepository invitationRepository,
            IMapper mapper,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _invitationRepository = invitationRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<InvitationDto>> Handle(GetUserInvitationsQuery getUserInvitationsQuery, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeAccessOrThrow(_httpContextAccessor.HttpContext, getUserInvitationsQuery.UserId);
            var invitations = await _invitationRepository.GetUsersInvitationsAsync(getUserInvitationsQuery.UserId);
            var mappedInvitations = invitations.Select(x => _mapper.Map<InvitationDto>(x)).ToList(); 
            return mappedInvitations;
        }
    }
}

