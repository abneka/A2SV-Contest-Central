namespace Application.DTOs.Contest;

public class BarGraphDto
{
    // list of (x, y) pairs
    public List<GraphDataPointsDto> BarGraphData { get; set; } = null!;
    public int StudentsNumber { get; set; }
}