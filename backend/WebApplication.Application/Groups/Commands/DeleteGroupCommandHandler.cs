using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Groups.Commands
{
    public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, string>
    {
        private readonly IGroupRepository _groupRepository;
        public DeleteGroupCommandHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<string> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            await _groupRepository.DeleteGroupAsync(request.Id);
            return request.Id;
        }
    }
}
