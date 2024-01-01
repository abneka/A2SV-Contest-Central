using Application.DTOs.Common;

namespace Application.DTOs.ContestGroup;

public class ContestGroupRequestDto : BaseDto
{
    public Guid ContestId { get; set; }
    public Guid GroupId { get; set; }
}