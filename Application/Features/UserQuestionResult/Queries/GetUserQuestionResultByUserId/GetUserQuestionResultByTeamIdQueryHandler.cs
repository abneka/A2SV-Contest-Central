using Application.Contracts.Persistence;
using Application.DTOs.TeamQuestionResult;
using Application.DTOs.UserQuestionResult;
using Application.Features.TeamQuestionResult.Queries.GetTeamQuestionResultByTeamId;
using AutoMapper;
using MediatR;

namespace Application.Features.UserQuestionResult.Queries.GetUserQuestionResultByUserId;

public class GetUserQuestionResultByTeamIdQueryHandler : IRequestHandler<GetUserQuestionResultByTeamIdQuery, List<UserQuestionsResultResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserQuestionResultByTeamIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<UserQuestionsResultResponseDto>> Handle(GetUserQuestionResultByTeamIdQuery request, CancellationToken cancellationToken)
    {
        var questionResult =
            await _unitOfWork.UserQuestionResultRepository.GetByUserIdAsync(request.TeamId);
        return _mapper.Map<List<UserQuestionsResultResponseDto>>(questionResult);
    }
}