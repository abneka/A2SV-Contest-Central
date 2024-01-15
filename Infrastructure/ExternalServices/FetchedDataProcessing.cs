using Application.Contracts.Infrastructure.ExternalServices;
using Application.Models;
using Microsoft.Extensions.Options;

namespace Infrastructure.ExternalServices;

public class FetchedDataProcessing : IFetchedDataProcessing
{
    private dynamic response ;
    private readonly CodeforcesApiService codeforcesApi;
    

    public FetchedDataProcessing(IOptions<CodeforcesAPISettings> codeforcesAPISettings, HttpClient httpClient)
    {
        this.codeforcesApi = new CodeforcesApiService
        {
            codeforcesAPISettings = codeforcesAPISettings.Value,
            httpClient = httpClient
        };
    }

    public async void FetchContestData(string contest_id)
    {
        response = await codeforcesApi.GetContestData(contest_id);    
    }

    public async Task<FetchedContest> GetContestData()
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<FetchedQuestion>> GetContestQuestions()
    {
        throw new NotImplementedException();
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

        return fetchedUserContestResults.AsReadOnly();
    }

}


 