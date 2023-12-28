using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.DTOs.Contest;
using Application.Exceptions;
using Application.Features.Contest.Command.Create;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Contest.Commands.CreateContest;

public class CreateContestCommandHandler : IRequestHandler<CreateContestCommand,ContestResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public CreateContestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ContestResponseDto> Handle(CreateContestCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateContestCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        // insert Universities
        // insert Countries
        // await _unitOfWork.Questions.CreateAsync(command.UpdateContest.Questions);
        // await _unitOfWork.Groups.CreateAsync(command.UpdateContest.Groups);
         
        var new_contest = new ContestEntity();
        new_contest.ContestGlobalId = ParseIdFromUrl(command.NewContest.ContestUrl);
        new_contest.Name = command.NewContest.Name;
        new_contest.ContestUrl = command.NewContest.ContestUrl;
        var createdContest = await _unitOfWork.ContestRepository.CreateAsync(new_contest);

        return _mapper.Map<ContestResponseDto>(createdContest);
    }

    public static string ParseIdFromUrl(string url)
    {
        url = Uri.UnescapeDataString(url);
        int index = url.LastIndexOf('/');
        string id = url.Substring(index + 1);

        return id;
    }
}