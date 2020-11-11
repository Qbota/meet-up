using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Application.Groups.Models;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Groups.Queries
{
    public class GetGroupQueryHandler : IRequestHandler<GetGroupQuery, GroupDto>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetGroupQueryHandler(
            IGroupRepository groupRepository,
            IMapper mapper,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _groupRepository = groupRepository;
            _mapper = mapper;
        }
        public async Task<GroupDto> Handle(GetGroupQuery getGroupQuery, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeGroupAccessOrThrowAsync(_httpContextAccessor.HttpContext, getGroupQuery.Id);
            var group = await _groupRepository.GetGroupByIdAsync(getGroupQuery.Id);
            var mappedGroup = _mapper.Map<GroupDO, GroupDto>(group);
            return mappedGroup;
        }
    }
}
