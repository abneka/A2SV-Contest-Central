namespace Application.DTOs.OverallStatus;

public class OverallStatusResponseDto
{
    public string? Year { get; set; }
    public int TotalContest { get; set; }
    public int TotalGroup { get; set; }
    public int TotalMembers { get; set; }
    public int TotalMinutes { get; set; }
}