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

    public async Task<IReadOnlyList<FetchedUserContestResult>> GetUserContestResults()
    {
        throw new NotImplementedException();
    }


    async Task IFetchedDataProcessing.FetchContestData(string contestId)
    {
        throw new NotImplementedException();
    }
}