using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Features.CodeforcesApi.Queries.CheckContestUrl
{
    public class ContestUrlCheckerRequestValidator : AbstractValidator<ContestUrlCheckerRequest>
    {
        public ContestUrlCheckerRequestValidator()
        {
            RuleFor(request => request.ContestUrl)
                .NotEmpty().WithMessage("Contest URL cannot be empty.")
                .Must(BeValidCodeforcesContestUrl).WithMessage("Invalid Codeforces contest URL.");
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
