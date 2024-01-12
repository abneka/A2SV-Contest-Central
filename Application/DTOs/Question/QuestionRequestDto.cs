namespace Application.DTOs.Question
{
    public class QuestionRequestDto
    {
        public Guid ContestId { get; set; }
        public string ContestUrl { get; set; } = string.Empty;
        public IReadOnlyList<QuestionInfoDto> Questions { get; set; } = null!;
    }
}
