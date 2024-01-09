namespace Application.DTOs.Question;

public class QuestionForUserDto
{
    public double Points { get; set; }
    
    public int RejectedAttemptCount { get; set; }
    
    public string BestSubmissionTimeSeconds { get; set; } = null!;
    
    public string GlobalQuestionUrl { get; set; } = null!;

    public string Name { get; set; } = string.Empty;

    public string Index { get; set; } = string.Empty;
    public bool Solved { get; set; } = false;
}