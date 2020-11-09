using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Mongo.Models;
using WebApplication.Mongo.Repositories;

namespace WebApplication.Application.Groups.Commands
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, string>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        public UpdateGroupCommandHandler(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }
        public async Task<string> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupByIdAsync(request.ID);
            var changes = _mapper.Map<GroupDO>(request);
            var updated = _mapper.Map(changes, group);
            await _groupRepository.UpdateGroupAsync(updated);
            return request.ID;
        }
    }
}
