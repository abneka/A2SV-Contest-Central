using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Contest.Commands.UpdateContest;

public class UpdateContestCommandHandler : IRequestHandler<UpdateContestCommand,Unit>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateContestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateContestCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateContestCommandValidator(_unitOfWork);
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    
        var old_contest = await _unitOfWork.ContestRepository.GetByIdAsync(command.ContestId);
        if (old_contest == null)
        {
            throw new NotFoundException($"Contest with id {command.ContestId} does't exist!", command);
        }

        // if(old_contest.Status) return Unit.Value;

        old_contest.Name = command.UpdateContest.ContestName;
        old_contest.ContestUrl = command.UpdateContest.ContestUrl;

        // await _unitOfWork.Questions.UpdateAsync(command.ContestId, command.UpdateContest.Questions);
        // await _unitOfWork.Groups.UpdateAsync(command.ContestId, command.UpdateContest.Groups);
        
        // use transaction to make sure that all questions and the contest are updated
        
        await _unitOfWork.ContestRepository.UpdateAsync(command.ContestId, old_contest);
        return Unit.Value;
    }
}


