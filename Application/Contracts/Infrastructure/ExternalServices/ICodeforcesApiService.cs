namespace Application.Contracts.Infrastructure.ExternalServices
{
    public interface ICodeforcesApiService
    {
        public Task<string> GetContestData(string contestId);
    }
}
