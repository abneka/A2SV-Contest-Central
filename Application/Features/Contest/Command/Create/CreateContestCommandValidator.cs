using System.Text.RegularExpressions;
using Application.Contracts.Infrastructure.ExternalServices;
using Application.Contracts.Persistence;
using Application.DTOs.Contest;
using FluentValidation;

namespace Application.Features.Contest.Commands.CreateContest;

public class CreateContestCommandValidator : AbstractValidator<ContestInfoRequestDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private IFetchedDataProcessing _codeforcesApiService;

    public CreateContestCommandValidator(IUnitOfWork unitOfWork,IFetchedDataProcessing codeforcesApiService)
    {
        _unitOfWork = unitOfWork;
        _codeforcesApiService = codeforcesApiService;

        RuleFor(dto => dto.ContestName)
            .NotEmpty()
            .WithMessage("Contest name is required.")
            .MaximumLength(255)
            .WithMessage("Contest name cannot exceed 255 characters.");

        RuleFor(dto => dto.ContestUrl)
            .NotEmpty()
            .WithMessage("Contest URL is required.")
            .MaximumLength(255)
            .WithMessage("Contest URL cannot exceed 255 characters.")
            .Must(BeValidCodeforcesContestUrl)
            .WithMessage("Invalid Codeforces contest URL.")
            .MustAsync(IsContestCreatedBefore)
            .WithMessage(
                "A contest with the given Codeforces URL already exists. Please provide a unique URL for the new contest."
            )
            .MustAsync(IsContestCreatedOnCodeforcesAsync)
            .WithMessage("Contest didn't created in codeforces");

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

    private async Task<bool> IsContestCreatedBefore(string contest_url,CancellationToken cancellationToken)
    {
        string contest_id = ParseIdFromUrl(contest_url);
        if (contest_id == string.Empty)
            return false;

        bool contest = await _unitOfWork.ContestRepository.ExistsContestGlobalIdAsync(contest_id);
        return !contest;
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

    private async Task<bool> IsContestCreatedOnCodeforcesAsync(string url, CancellationToken cancellationToken)
        {
            try
            {
                //parse id from contest url
                string contest_id = ParseIdFromUrl(url);

                //fetch data from codeforces using codeforces api
                // dynamic data = await _codeforcesApiService.GetContestData(contest_id);
                // TODO: work on this: @mieraf
                dynamic data = null;

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
}
