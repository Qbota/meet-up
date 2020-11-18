using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Groups.Commands
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, string>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateGroupCommandHandler(
            IGroupRepository groupRepository, 
            IMapper mapper,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _groupRepository = groupRepository;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<string> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeGroupAccessOrThrowAsync(_httpContextAccessor.HttpContext, request.ID);
            var group = await _groupRepository.GetGroupByIdAsync(request.ID);
            var changes = _mapper.Map<GroupDO>(request);
            var updated = _mapper.Map(changes, group);
            await _groupRepository.UpdateGroupAsync(updated);
            return request.ID;
        }
    }
}
