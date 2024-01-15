using Application.Models;

namespace Application.Contracts.Infrastructure.ExternalServices
{
    public interface IFetchedDataProcessing
    {
        public Task FetchContestData(string contestId);
        public Task<FetchedContest> GetContestData();
        public Task<IReadOnlyList<FetchedQuestion>> GetContestQuestions();
        
        public Task<IReadOnlyList<FetchedUserContestResult>> GetUserContestResults();
        
    }
}
