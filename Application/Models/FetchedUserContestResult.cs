namespace Application.Models;

public class FetchedUserContestResult
{
    // User Data
    public string TeamId { get; set; } = string.Empty;
    public string TeamName { get; set; } = string.Empty;
    public List<string> Handles { get; set; } = new List<string>();
    
    // Contest Result
    public int ContestId { get; set; }
    public int Rank { get; set; }
    public int Points { get; set; }
    public int Penalty { get; set; }
    public int SuccessfulHackCount { get; set; }
    public int UnsuccessfulHackCount { get; set; }
    
    // Question Results
    List<FetchedUserQuestionResult> QuestionResults { get; set; } = new List<FetchedUserQuestionResult>();
}