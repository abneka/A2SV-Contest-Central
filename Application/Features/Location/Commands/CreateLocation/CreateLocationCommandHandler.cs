using Application.Contracts.Persistence;
using Application.DTOs.Location;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Location.Commands.CreateLocation;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, LocationResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork; 

    public CreateLocationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<LocationResponseDto> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLocationCommandValidator();
        var validationResult = validator.Validate(request);
        
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);
        
        var location = _mapper.Map<LocationEntity>(request.LocationDto);
        var created = await _unitOfWork.LocationRepository.CreateAsync(location);
        
        return _mapper.Map<LocationResponseDto>(created);
    }
}