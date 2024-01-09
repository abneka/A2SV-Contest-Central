using Application.DTOs.Common;
using Application.DTOs.Question;
using Application.DTOs.User;
using Domain.Entities;

namespace Application.DTOs.Contest;

public class UserContestAndQuestionDto : ContestResultDto
{
    public Guid UserId { get; set; }
    public int TotalSubmissions { get; set; }
    public int WrongSubmissions { get; set; }
    public int ProblemSolved { get; set; }
    public double ConversionRatePercent { get; set; }
    public ContestUserDto User { get; set; } = null!;
    public List<QuestionForUserDto> Questions { get; set; } = null!;
}