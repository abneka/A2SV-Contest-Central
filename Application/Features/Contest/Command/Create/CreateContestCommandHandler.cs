using Application.Contracts.Infrastructure.ExternalServices;
using Application.Contracts.Persistence;
using Application.DTOs.Contest;
using Application.Features.Contest.Command.Create;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Contest.Commands.CreateContest;

public class CreateContestCommandHandler : IRequestHandler<CreateContestCommand, ContestResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFetchedDataProcessing _fetchedDataProcessing;

    public CreateContestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFetchedDataProcessing fetchedDataProcessing)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _fetchedDataProcessing = fetchedDataProcessing;
    }

    public async Task<ContestResponseDto> Handle(
        CreateContestCommand command,
        CancellationToken cancellationToken
    )
    {
        var validator = new CreateContestCommandValidator(_unitOfWork, _fetchedDataProcessing);
        var validationResult = await validator.ValidateAsync(command.NewContest, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var new_contest = new ContestEntity
        {
            ContestGlobalId = ParseIdFromUrl(command.NewContest.ContestUrl),
            Name = command.NewContest.ContestName,
            ContestUrl = command.NewContest.ContestUrl
        };

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
