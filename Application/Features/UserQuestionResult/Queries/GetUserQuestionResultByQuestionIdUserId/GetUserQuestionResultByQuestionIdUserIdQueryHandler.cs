using Application.Contracts.Persistence;
using Application.DTOs.Common;
using Application.DTOs.UserQuestionResult;
using Application.Exceptions;
using AutoMapper;
using MediatR;

namespace Application.Features.UserQuestionResult.Queries.GetUserQuestionResultByQuestionIdUserId;

public class GetUserQuestionResultByQuestionIdUserIdQueryHandler : IRequestHandler<GetUserQuestionResultByQuestionIdUserIdQuery, UserQuestionsResultResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserQuestionResultByQuestionIdUserIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserQuestionsResultResponseDto> Handle(GetUserQuestionResultByQuestionIdUserIdQuery request, CancellationToken cancellationToken)
    {
        var isUserExist = await _unitOfWork.UserRepository.Exists(request.UserId);
        if (isUserExist is false)
        {
            throw new NotFoundException(nameof(User), request.UserId);
        }
        
        // var isQuestionExist = await _unitOfWork.QuestionRepository.Exists(request.QuestionId);
        
        var userQuestionResult = await _unitOfWork.UserQuestionResultRepository.GetUserQuestionResultByQuestionIdUserId(request.QuestionId, request.UserId);
        
        return _mapper.Map<UserQuestionsResultResponseDto>(userQuestionResult);
    }
}