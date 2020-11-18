using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Application.Groups.Models;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Groups.Queries
{
    public class GetGroupsQueryHandler : IRequestHandler<GetGroupsQuery, IEnumerable<GroupDto>>
    {

        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetGroupsQueryHandler(
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
        public async Task<IEnumerable<GroupDto>> Handle(GetGroupsQuery getGroupsQuery, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetGroupsAsync();
            if (groups != null && groups.Any())
            {
                SearchByParams(ref groups, getGroupsQuery);
                var list = groups.Select(x => _mapper.Map<GroupDto>(x)).ToList();
                _authorizationService.FilterResultByUserRightsAsync(_httpContextAccessor.HttpContext, ref list);
                return list;
            }
            return new List<GroupDto>();
        }

        private void SearchByParams(ref IEnumerable<GroupDO> groups, GetGroupsQuery getGroupsQuery)
        {
            if (!String.IsNullOrEmpty(getGroupsQuery.Name))
            {
                groups = groups.Where(x => x.Name.ToLower().Contains(getGroupsQuery.Name.ToLower()));
            }
            if (getGroupsQuery.MemberIDs != null && getGroupsQuery.MemberIDs.Any())
            {
                groups = groups.Where(x => x.MemberIDs.Select(x => getGroupsQuery.MemberIDs.Contains(x)).Any());
            }
        }
    }
}
