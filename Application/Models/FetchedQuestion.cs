namespace Application.Models;

public class FetchedQuestion
{
    public int ContestId { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string Index { get; set; } = string.Empty;
    
    public int Rating { get; set; }
}