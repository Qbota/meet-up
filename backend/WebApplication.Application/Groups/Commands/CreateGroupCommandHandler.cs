using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Exceptions;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Groups.Commands
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, string>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateGroupCommandHandler(
            IGroupRepository groupRepository,
            IUserRepository userRepository,
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = _mapper.Map<GroupDO>(request);

            if (group.MemberIDs is null)
                group.MemberIDs = new List<string>();
            var user = _httpContextAccessor.HttpContext.Items["Account"] as UserDO;
            if (user == null)
            {
                throw new AuthorizationException();
            }
            group.MemberIDs.ToList().Add(user.ID);
            await _groupRepository.AddGroupAsync(group);
            await UpdateUsersAsync(group);
            return group.ID;
        }

        private async Task UpdateUsersAsync(GroupDO group)
        {
            foreach (var id in group.MemberIDs)
            {
                var user = await _userRepository.GetUserByIdAsync(id);
                user.GroupIDs.ToList().Add(group.ID);
                await _userRepository.UpdateUserAsync(user);
            }
        }
    }
}
