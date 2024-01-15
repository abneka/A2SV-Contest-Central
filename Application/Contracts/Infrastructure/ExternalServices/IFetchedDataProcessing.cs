using Application.Models;

namespace Application.Contracts.Infrastructure.ExternalServices
{
    public interface IFetchedDataProcessing
    {
        public Task FetchContestData(string contestId);
        public FetchedContest GetContestData();
        public IReadOnlyList<FetchedQuestion> GetContestQuestions();
        
        public Task<IReadOnlyList<FetchedUserContestResult>> GetUserContestResults();
        
    }
}
