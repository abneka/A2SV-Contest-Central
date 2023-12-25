using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Location.Commands.UpdateLocation;

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateLocationCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateLocationCommandValidator();
        var validationResult = validator.Validate(request);
        
        if (validationResult.Errors.Count > 0)
            throw new ValidationException(validationResult.Errors);
       
        var toBeUpdatedLocation = await _unitOfWork.LocationRepository.GetByIdAsync(request.Id);
        if (toBeUpdatedLocation == null)
            throw new NotFoundException(nameof(toBeUpdatedLocation), request.Id);
        var location = _mapper.Map(request.LocationDto, toBeUpdatedLocation);
        await _unitOfWork.LocationRepository.UpdateAsync(location.Id, location);

        return Unit.Value;
    }
}