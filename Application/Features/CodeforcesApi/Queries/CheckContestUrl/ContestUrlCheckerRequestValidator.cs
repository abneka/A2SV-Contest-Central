using System.Text.RegularExpressions;
using Application.Contracts.Infrastructure.ExternalServices;
using Application.Contracts.Persistence;
using FluentValidation;

namespace Application.Features.CodeforcesApi.Queries.CheckContestUrl
{
    public class ContestUrlCheckerRequestValidator : AbstractValidator<ContestUrlCheckerRequest>
    {
        private IUnitOfWork _unitOfWork;
        private ICodeforcesApiService _codeforcesApiService;

        public ContestUrlCheckerRequestValidator(
            IUnitOfWork unitOfWork,
            ICodeforcesApiService codeforcesApiService
        )
        {
            _unitOfWork = unitOfWork;
            _codeforcesApiService = codeforcesApiService;
            RuleFor(request => request.ContestUrl)
                .NotEmpty()
                .WithMessage("Contest URL cannot be empty.")
                .Must(BeValidCodeforcesContestUrl)
                .WithMessage("Invalid Codeforces contest URL.")
                .MustAsync(IsContestCreatedOnCodeforcesAsync)
                .WithMessage("Contest didn't created in codeforces")
                .MustAsync(IsContestCreatedBefore)
                .WithMessage(
                    "A contest with the given Codeforces URL already exists. Please provide a unique URL for the new contest."
                );
        }

        private async Task<bool> IsContestCreatedBefore(
            string contest_url,
            CancellationToken cancellationToken
        )
        {
            string contest_id = ParseIdFromUrl(contest_url);
            if (contest_id == string.Empty)
                return false;

            bool contest = await _unitOfWork.ContestRepository.ExistsContestGlobalIdAsync(
                contest_id
            );
            return !contest;
        }

        private async Task<bool> IsContestCreatedOnCodeforcesAsync(
            string url,
            CancellationToken cancellationToken
        )
        {
            try
            {
                //parse id from contest url
                string contest_id = ParseIdFromUrl(url);

                //fetch data from codeforces using codeforces api
                dynamic data = await _codeforcesApiService.GetContestData(contest_id);

                if (data == null)
                    return false;

                if (data.status == "FAILED")
                {
                    if (data.comment == $"contestId: Contest with id {contest_id} not found")
                        return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching data from Codeforces", ex);
                throw new Exception("An error occurred while fetching data from Codeforces");
            }
        }

        private string ParseIdFromUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return string.Empty;
            }

            url = url.Trim();
            url = Uri.UnescapeDataString(url);

            int index = url.LastIndexOf('/');

            if (index == -1 || index == url.Length - 1)
            {
                return string.Empty;
            }

            string id = url.Substring(index + 1);

            return id;
        }

        private bool BeValidCodeforcesContestUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }
            url = Uri.UnescapeDataString(url);

            // Define the regex pattern for Codeforces contest URLs
            var pattern = @"^https://codeforces\.com/(gym|contests)/\d+$";
            // Use regex to match the pattern
            return Regex.IsMatch(url, pattern, RegexOptions.IgnoreCase);
        }
    }
}
