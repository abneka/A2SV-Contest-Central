using Application.Contracts.Persistence;
using Application.DTOs.Question;
using AutoMapper;
using MediatR;

namespace Application.Features.Question.Queries.GetSingle;

public class GetSingleQuestionHandler : IRequestHandler<GetSingleQuestionRequest, QuestionResponseDto>
{
    public readonly IQuestionRepository _questionRepository;
    public readonly IMapper _mapper;
    
    public GetSingleQuestionHandler(IQuestionRepository questionRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }
    
    public async Task<QuestionResponseDto> Handle(GetSingleQuestionRequest request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetByIdAsync(request.QuestionId);
        
        return _mapper.Map<QuestionResponseDto>(question);
    }
}