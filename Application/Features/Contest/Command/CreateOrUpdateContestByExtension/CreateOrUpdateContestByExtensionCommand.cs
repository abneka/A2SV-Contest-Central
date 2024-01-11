using Application.DTOs.Contest;
using Application.DTOs.Contest.CodeforcesExtension;
using MediatR;

namespace Application.Features.Contest.Command.CreateOrUpdateContestByExtension
{
    public class CreateOrUpdateContestByExtensionCommand : IRequest<ContestExtResponseDto>
    {
        public ContestInfoRequestDto NewContest { get; set; } = null!;
    }
}
