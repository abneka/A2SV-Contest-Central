﻿using Application.Contracts.Persistence;
using Application.DTOs.GlobalQuestion;
using MediatR;
using AutoMapper;

namespace Application.Features.Question.Queries.CheckDuplicate;

public class CheckDuplicateQuestionHandler : IRequestHandler<CheckDuplicateQuestionRequest, GlobalQuestionDto>
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IMapper _mapper;
    
    public CheckDuplicateQuestionHandler(IQuestionRepository questionRepository, IMapper mapper)
    {
        _questionRepository = questionRepository;
        _mapper = mapper;
    }
    
    public async Task<GlobalQuestionDto> Handle(CheckDuplicateQuestionRequest request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.ExistsByGlobalQuestionUrl(request.GlobalQuestionUrl);
        
        return _mapper.Map<GlobalQuestionDto>(question);
    }
    
    
}