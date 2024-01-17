using Application.Contracts.Infrastructure.ExternalServices;
using Application.Models;
using Microsoft.Extensions.Options;

namespace Infrastructure.ExternalServices;

public class FetchedDataProcessing : IFetchedDataProcessing
{
    private dynamic response = null!;

    public FetchedDataProcessing(IOptions<CodeforcesAPISettings> codeforcesAPISettings, HttpClient httpClient)
    {

        CodeforcesApiService.codeforcesAPISettings = codeforcesAPISettings.Value;
        CodeforcesApiService.httpClient = httpClient;
        
    }

    public async Task<bool> IsHandleValid(string handle){
        return await CodeforcesApiService.IsHandleValid(handle);
    }

    public async Task FetchContestData(string contest_id)
    {
        response = await CodeforcesApiService.GetContestData(contest_id);
    }

    public FetchedContest GetContestData()
    {
        var contest = response.result?.contest;
        
        if (contest == null)
            throw new Exception("Contest data is null");
        
        var contestData = new FetchedContest
        {
            Name = contest.name ?? string.Empty,
            Type = contest.type ?? string.Empty,
            Status = contest.phase ?? string.Empty,
            DurationSeconds = contest.durationSeconds ?? 0,
            StartTimeSeconds = contest.startTimeSeconds ?? 0,
            RelativeTimeSeconds = contest.relativeTimeSeconds ?? 0,
            PreparedBy = contest.preparedBy ?? string.Empty,
            WebsiteUrl = contest.websiteUrl ?? string.Empty,
            Description = contest.description??string.Empty,
            Difficulty = contest.difficulty ?? string.Empty,
            Kind = contest.kind ?? string.Empty,
            Season = contest.season ?? string.Empty
        };
        
        return contestData;
}

    public IReadOnlyList<FetchedQuestion> GetContestQuestions()
    {
        var questions = response.result?.problems;
        
        if (questions == null)
            throw new Exception("Questions data is null");
        
        var contestQuestions = new List<FetchedQuestion>();

        foreach (var question in questions)
        {
            var contestQuestion = new FetchedQuestion
            {
                Name = question.name ?? string.Empty,
                Rating = question.rating ?? 0,
                ContestId = question.contestId ?? string.Empty,
                Index = question.index ?? string.Empty,
            };
            
            contestQuestions.Add(contestQuestion);
        }

        return contestQuestions;
    }

    public IReadOnlyList<FetchedUserContestResult> GetUserContestResults()
    {
        List<FetchedUserContestResult> fetchedUserContestResults = new List<FetchedUserContestResult>();

        foreach (var row in response.rows)
        {
            FetchedUserContestResult userContestResult = new FetchedUserContestResult
            {
                ContestId = row.party.contestId,
                Rank = row.rank,
                Points = (float)row.points,
                Penalty = row.penalty,
                SuccessfulHackCount = row.successfulHackCount,
                UnsuccessfulHackCount = row.unsuccessfulHackCount,
                TeamName = row.party.teamName ?? string.Empty,
                TeamId = row.party.teamId ?? string.Empty
            };

            
            foreach(var member in row.party.members){
                userContestResult.Handles.Add(member);
            }

            foreach (var problemResult in row.problemResults)
            {
                int index = row.ProblemResults.IndexOf(problemResult);
                string charIndex = ((char)('A' + index)).ToString();

                FetchedUserQuestionResult questionResult = new FetchedUserQuestionResult
                {
                    Index = charIndex,
                    Points = problemResult.points,
                    RejectedAttemptCount = problemResult.rejectedAttemptCount,
                    BestSubmissionTimeSeconds = problemResult.bestSubmissionTimeSeconds
                };

                userContestResult.QuestionResults.Add(questionResult);
            }

            fetchedUserContestResults.Add(userContestResult);
        }

        return fetchedUserContestResults;
    }

    public bool IsContestValid(string contest_id)
    {
        if (response == null)
            return false;

        if (response.status == "FAILED")
        {
            if (response.comment == $"contestId: Contest with id {contest_id} not found")
                return false;
        }
        return true;
    }
}