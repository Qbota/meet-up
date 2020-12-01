using MediatR;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Application.Authorization;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Groups.Commands
{
    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, string>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        public DeleteGroupCommandHandler(
            IGroupRepository groupRepository, 
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        public async Task<string> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeGroupAccessOrThrow(_httpContextAccessor.HttpContext, request.Id);
            await _groupRepository.DeleteGroupAsync(request.Id);
            await UpdateUsersAsync(request.Id);
            return request.Id;
        }
        private async Task UpdateUsersAsync(string groupId)
        {
            var users = await _userRepository.GetUsersByGroupIdAsync(groupId);
            foreach (var user in users)
            {
                user.GroupIDs.ToList().Remove(groupId);
                await _userRepository.UpdateUserAsync(user);
            }
        }
    }
}
