using Application.Contracts.Persistence;
using Application.DTOs.Contest.CodeforcesExtension;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Contest.Command.CreateOrUpdateContestByExtension
{
    public class CreateOrUpdateContestByExtensionCommandHandler
        : IRequestHandler<CreateOrUpdateContestByExtensionCommand, ContestExtResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrUpdateContestByExtensionCommandHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper
        )
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ContestExtResponseDto> Handle(
            CreateOrUpdateContestByExtensionCommand command,
            CancellationToken cancellationToken
        )
        {
            string contest_id = ParseIdFromUrl(command.NewContest.ContestUrl);
            var old_contest = await _unitOfWork.ContestRepository.GetContestByGlobalIdAsync(contest_id);

            var new_contest = new ContestEntity
            {
                ContestGlobalId = contest_id,
                Name = command.NewContest.ContestName,
                ContestUrl = command.NewContest.ContestUrl
            };

            var res = new ContestExtResponseDto();

            if(old_contest == null){
                var created_contest = await _unitOfWork.ContestRepository.CreateAsync(new_contest);
                res.Status = "OK";
                res.Result = "Created";
                res.Message = "contest created successfully";
                return res;
            }

            if(old_contest.Status == "FINISHED"){
                res.Status = "Failed";
                res.Result = "AGAIN";
                res.Message = "contest already created and fetch before, create new contest!";
                return res;
            }

            var updated_contest = await _unitOfWork.ContestRepository.UpdateContestByGlobalIdAsync(old_contest.Id, new_contest);
            res.Status = "OK";
            res.Result = "Updated";
            res.Message = "contest updated successfully";

            return res;
        }

        public static string ParseIdFromUrl(string url)
        {
            url = Uri.UnescapeDataString(url);
            int index = url.LastIndexOf('/');
            string id = url.Substring(index + 1);

            return id;
        }
    }
}
