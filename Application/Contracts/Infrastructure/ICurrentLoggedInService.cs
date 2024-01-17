namespace Application.Contracts.Infrastructure
{
    public interface ICurrentLoggedInService
    {
        Guid GetCurrentLoggedInId();

        string GetCurrentLoggedInUsername();

        string GetCurrentLoggedInEmail();

        string GetCurrentLoggedInRole();
        int GetCurrentLoggedInPriority();
    }
}
