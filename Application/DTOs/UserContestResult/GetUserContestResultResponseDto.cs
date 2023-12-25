using Application.DTOs.Common;

namespace Application.DTOs.UserContestResult;

public class GetUserContestResultResponseDto : BaseDto
{
    public Guid UserId { get; set; }
    public Guid ContestId { get; set; }
    public float Points { get; set; }
    public int Rank { get; set; }
    public int Penalty { get; set; }
    public int SuccessfulHackCount { get; set; }
    public int UnsuccessfulHackCount { get; set; }
    public bool IsVirtual { get; set; }
}