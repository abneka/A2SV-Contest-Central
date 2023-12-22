using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.Contest.Commands.DeleteContest;

public class DeleteContestCommandHandler : IRequestHandler<DeleteContestCommand,Unit>
{
    private readonly IMapper _mapper;
    private readonly IContestRepository _contestRepository;
    public DeleteContestCommandHandler(IContestRepository contestRepository, IMapper mapper)
    {
        _mapper = mapper;
        _contestRepository = contestRepository;
    }

    public async Task<Unit> Handle(DeleteContestCommand command, CancellationToken cancellationToken)
    {
        bool res = await _contestRepository.Exists(command.ContestId);
        if(res == false)
             throw new NotFoundException($"Contest with id {command.ContestId} does't exist!", command);
             
        var contest =  await _contestRepository.DeleteAsync(command.ContestId);
        return Unit.Value;
    }
}