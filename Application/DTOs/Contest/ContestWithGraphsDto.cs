namespace Application.DTOs.Contest;

public class ContestWithGraphsDto
{
    public List<UserContestAndQuestionDto> Leaderboard { get; set; } = null!;
    public BarGraphDto BarGraph { get; set; } = null!;
    public PieChartDto PieChart { get; set; } = null!;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}