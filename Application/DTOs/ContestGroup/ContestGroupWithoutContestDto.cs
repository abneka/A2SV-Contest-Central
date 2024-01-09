using Application.DTOs.Group;
using Domain.Entities;

namespace Application.DTOs.ContestGroup;

public class ContestGroupWithoutContestDto
{
    public Guid GroupId { get; set; }
    public GroupDto Group { get; set; } = null!;
}