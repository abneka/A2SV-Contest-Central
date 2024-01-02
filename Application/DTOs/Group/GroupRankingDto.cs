namespace Application.DTOs.Group;

public class GroupRankingDto : GroupResponseDto
{
    public int NumberOfProblemsTaken { get; set; }
    public int NumberOfProblemsSolved { get; set; }
    public int AverageNumberOfProblemsSolved { get; set; }
    public double ContestConversionRate { get; set; }
    public int Rank { get; set; }
}