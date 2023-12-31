using System.Text.RegularExpressions;
using Application.Contracts.Persistence;
using Application.DTOs.Contest;
using FluentValidation;

namespace Application.Features.Contest.Commands.CreateContest;

public class CreateContestCommandValidator : AbstractValidator<ContestRequestDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateContestCommandValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

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
            );

        RuleFor(dto => dto.Questions)
            .NotNull()
            .WithMessage("Questions list cannot be null.")
            .NotEmpty()
            .WithMessage("At least one question is required.");

        RuleFor(dto => dto.Groups)
            .NotNull()
            .WithMessage("Groups list cannot be null.")
            .NotEmpty()
            .WithMessage("At least one group is required.");
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

    private async Task<bool> IsContestCreatedBefore(
        string contest_url,
        CancellationToken cancellationToken
    )
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
}
