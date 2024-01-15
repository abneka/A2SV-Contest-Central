namespace Application.Models;

public class FetchedUserQuestionResult
{
    public string Index { get; set; } = string.Empty;
    public double Points { get; set; }
    public int RejectedAttemptCount { get; set; }
    public int BestSubmissionTimeSeconds { get; set; }
}