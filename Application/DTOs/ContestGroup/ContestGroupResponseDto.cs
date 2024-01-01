using Application.DTOs.Common;

namespace Application.DTOs.ContestGroup;

public class ContestGroupResponseDto : BaseDto
{
    public Guid ContestId { get; set; }
    public Guid GroupId { get; set; }
}