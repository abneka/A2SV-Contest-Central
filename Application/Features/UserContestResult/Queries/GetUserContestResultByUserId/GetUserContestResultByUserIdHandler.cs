using Application.Contracts.Persistence;
using Application.DTOs.UserContestResult;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Features.UserContestResult.Queries.GetUserContestResultByUserId;

public class GetUserContestResultByUserIdHandler : IRequestHandler<GetUserContestResultByUserId, UserContestResultResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetUserContestResultByUserIdHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserContestResultResponseDto> Handle(GetUserContestResultByUserId request, CancellationToken cancellationToken)
    {
        var validator = new GetUserContestResultByUserIdValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var userContestResult = await _unitOfWork.UserContestResultRepository.GetUserContestResultByUserIdAndContestIdAsync(request.UserId, request.ContestId);
        return _mapper.Map<UserContestResultResponseDto>(userContestResult);
    }
}