using Application.Contracts.Persistence;
using Application.DTOs.Team;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Team.Commands;

public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, TeamResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTeamCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<TeamResponseDto> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateTeamCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var team = _mapper.Map<TeamEntity>(request.Team);
        var result = await _unitOfWork.TeamRepository.CreateAsync(team);
        
        return _mapper.Map<TeamResponseDto>(team);
    }
}