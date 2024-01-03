namespace Application.DTOs.Contest;

public class PaginatedContestResponseDto
{
    public List<ContestResponseDto> ContestsList { get; set; } = null!;
    public int ItemsCount { get; set; } = 1;
    public int PageNumber { get; set; } = 10;
}