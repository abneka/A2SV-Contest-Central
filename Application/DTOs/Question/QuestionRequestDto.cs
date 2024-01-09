namespace Application.DTOs.Question
{
    public class QuestionRequestDto
    {
        public string ContestId { get; set; } = null!;
        public IReadOnlyList<string> Questions { get; set; } = null!;
    }
}
