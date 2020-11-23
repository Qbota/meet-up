using MediatR;
using Microsoft.AspNetCore.Http;
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
        public DeleteGroupCommandHandler(
            IGroupRepository groupRepository, 
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _groupRepository = groupRepository;
            _authorizationService = authorizationService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            _authorizationService.AuthorizeGroupAccessOrThrow(_httpContextAccessor.HttpContext, request.Id);
            await _groupRepository.DeleteGroupAsync(request.Id);
            return request.Id;
        }
    }
}
