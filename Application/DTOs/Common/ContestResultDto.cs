using Application.DTOs.Contest;

namespace Application.DTOs.Common;

public class ContestResultDto : BaseDto
{
    public Guid ContestId { get; set; }
    public float Points { get; set; }
    public int Rank { get; set; }
    public int Penalty { get; set; }
    public int SuccessfulHackCount { get; set; }
    public int UnsuccessfulHackCount { get; set; }
    public bool IsVirtual { get; set; }
}