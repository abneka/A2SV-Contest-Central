using Application.Contracts.Persistence;
using Application.DTOs.Question;
using AutoMapper;
using MediatR;

namespace Application.Features.Question.Queries.GetAll;

public class GetAllQuestionHandler : IRequestHandler<GetAllQuestionRequest, IReadOnlyList<QuestionResponseDto>>
{
    public readonly IMapper _mapper;
    public readonly IQuestionRepository _questionRepository;
    
    public GetAllQuestionHandler(IQuestionRepository questionRepository, IMapper mapper)
    {
        _mapper = mapper;
        _questionRepository = questionRepository;
    }
    
    public async Task<IReadOnlyList<QuestionResponseDto>> Handle(GetAllQuestionRequest request, CancellationToken cancellationToken)
    {
        var questions = await _questionRepository.GetAllAsync();
        return _mapper.Map<IReadOnlyList<QuestionResponseDto>>(questions);
    }
}