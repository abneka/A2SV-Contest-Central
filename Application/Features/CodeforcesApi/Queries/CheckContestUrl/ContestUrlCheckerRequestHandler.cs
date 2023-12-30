using System.Net.Http.Json;
using Application.Contracts.Infrastructure.ExternalServices;
using Application.Contracts.Persistence;
using FluentValidation;
using MediatR;
using Newtonsoft.Json;

namespace Application.Features.CodeforcesApi.Queries.CheckContestUrl
{
    public class ContestUrlCheckerRequestHandler : IRequestHandler<ContestUrlCheckerRequest, bool>
    {
        private IUnitOfWork _unitOfWork;
        private ICodeforcesApiService _codeforcesApiService;

        public ContestUrlCheckerRequestHandler(ICodeforcesApiService codeforcesApiService, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _codeforcesApiService = codeforcesApiService;
        }

        public async Task<bool> Handle(ContestUrlCheckerRequest request, CancellationToken cancellationToken)
        {
            var validator = new ContestUrlCheckerRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            //parse id from contest url
            string contest_id = ParseIdFromUrl(request.ContestUrl);

            try
            {
                //fetch data from codeforces using codeforces api
                dynamic data = await _codeforcesApiService.GetContestData(contest_id);

                if (data == null)
                    return false;

                if (data.status == "FAILED")
                {
                    if (data.comment == $"contestId: Contest with id {contest_id} not found")
                        return false;
                }
                else
                {
                    bool res = await _unitOfWork.ContestRepository.ExistsContestGlobalIdAsync(
                        contest_id
                    );
                    if (res)
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching data from Codeforces: {ex.Message}");
                return false;
            }
        }

        static string ParseIdFromUrl(string url)
        {
            url = Uri.UnescapeDataString(url);
            int index = url.LastIndexOf('/');
            string id = url.Substring(index + 1);

            return id;
        }
    }
}
