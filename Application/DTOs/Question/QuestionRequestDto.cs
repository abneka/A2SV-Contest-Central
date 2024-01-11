namespace Application.DTOs.Question
{
    public class QuestionRequestDto
    {
        public Guid ContestId {get; set; }
        public IReadOnlyList<QuestionInfoDto> Questions { get; set; } = null!;
    }
}
