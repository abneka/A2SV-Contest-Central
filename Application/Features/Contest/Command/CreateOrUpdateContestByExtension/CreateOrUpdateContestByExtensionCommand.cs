using Application.DTOs.Contest.CodeforcesExtension;
using MediatR;

namespace Application.Features.Contest.Command.CreateOrUpdateContestByExtension
{
    public class CreateOrUpdateContestByExtensionCommand : IRequest<ContestExtResponseDto>
    {
        public ContestExtRequestDto NewContest { get; set; } = null!;
    }
}
