using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class GetOverallStatus
{
    private readonly AppDBContext _dbContext;

    public GetOverallStatus(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<object> GetOverallStatusAsync()
    {
        // total contest between the dates September 2023 and August 2024 using createdAt
        var totalContest = await _dbContext.Contests.CountAsync(contest => contest.CreatedAt >= new DateTime(2023, 9, 1) && contest.CreatedAt <= new DateTime(2024, 8, 31));
        // total groups between the dates September 2023 and August 2024 using createdAt
        var totalGroups = await _dbContext.Groups.CountAsync(group => group.CreatedAt >= new DateTime(2023, 9, 1) && group.CreatedAt <= new DateTime(2024, 8, 31));
        // total questions between the dates September 2023 and August 2024 using createdAt
        var totalQuestions = await _dbContext.Questions.CountAsync(question => question.CreatedAt >= new DateTime(2023, 9, 1) && question.CreatedAt <= new DateTime(2024, 8, 31));
        // total members between the dates September 2023 and August 2024 using createdAt and userType is student
        var totalMembers = await _dbContext.Users.CountAsync(user => user.CreatedAt >= new DateTime(2023, 9, 1) && user.CreatedAt <= new DateTime(2024, 8, 31) && user.UserType.Name == "Student");
        
        // return using key-value
        return new
        {
            totalContest,
            totalGroups,
            totalQuestions,
            totalMembers
        };
    }
}