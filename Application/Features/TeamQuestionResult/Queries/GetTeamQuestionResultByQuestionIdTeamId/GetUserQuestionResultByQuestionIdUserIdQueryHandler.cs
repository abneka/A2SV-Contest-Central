using Application.Contracts.Persistence;
using Application.DTOs.Common;
using Application.DTOs.TeamQuestionResult;
using Application.Exceptions;
using Application.Features.UserQuestionResult.Queries.GetUserQuestionResultByQuestionIdUserId;
using AutoMapper;
using MediatR;

namespace Application.Features.TeamQuestionResult.Queries.GetTeamQuestionResultByQuestionIdTeamId;

public class GetUserQuestionResultByQuestionIdUserIdQueryHandler : IRequestHandler<GetUserQuestionResultByQuestionIdUserIdQuery, QuestionResultResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserQuestionResultByQuestionIdUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<QuestionResultResponseDto> Handle(GetUserQuestionResultByQuestionIdUserIdQuery request, CancellationToken cancellationToken)
    {
        var isUserExist = await _unitOfWork.UserRepository.Exists(request.UserId);
        if (isUserExist is false)
        {
            throw new NotFoundException(nameof(User), request.UserId);
        }
        
        // var isQuestionExist = await _unitOfWork.QuestionRepository.Exists(request.QuestionId);
        
        var userQuestionResult = await _unitOfWork.TeamQuestionResultRepository.GetTeamQuestionResultByQuestionIdTeamId(request.QuestionId, request.UserId);
        
        return _mapper.Map<TeamQuestionResultResponseDto>(userQuestionResult);
    }
}