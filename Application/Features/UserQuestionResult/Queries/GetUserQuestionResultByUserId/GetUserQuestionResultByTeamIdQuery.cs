using Application.DTOs.UserQuestionResult;
using MediatR;

namespace Application.Features.UserQuestionResult.Queries.GetUserQuestionResultByUserId;

public class GetUserQuestionResultByTeamIdQuery : IRequest<List<UserQuestionsResultResponseDto>>
{
    public Guid TeamId { get; set; }
}