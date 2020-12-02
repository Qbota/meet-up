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
        private readonly IInvitationRepository _invitationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CreateGroupCommandHandler(
            IGroupRepository groupRepository,
            IUserRepository userRepository,
            IInvitationRepository invitationRepository,
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _invitationRepository = invitationRepository;
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
                throw new AuthorizationException();

            if(!group.MemberIDs.ToList().Contains(user.ID))
                group.MemberIDs.ToList().Add(user.ID);

            await _groupRepository.AddGroupAsync(group);
            await UpdateCreatorAsync(group.ID, user);
            await InviteUsersAsync(group, user);
            return group.ID;
        }

        private async Task InviteUsersAsync(GroupDO group, UserDO creator)
        {
            var membersToInvite = group.MemberIDs.ToList();
            membersToInvite.Remove(creator.ID);
            foreach (var id in membersToInvite)
            {
                await _invitationRepository.AddInvitationAsync(new InvitationDO
                {
                    GroupId = group.ID,
                    GroupName = group.Name,
                    SenderName = creator.Name,
                    UserId = id
                });
            }
        }

        private async Task UpdateCreatorAsync(string groupId, UserDO user)
        {
            if (user.GroupIDs is null)
                user.GroupIDs = new List<string>();
           user.GroupIDs.ToList().Add(groupId);
           await _userRepository.UpdateUserAsync(user);
        }
    }
}
