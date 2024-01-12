using Domain.Common;

namespace Domain.Entities;

public class UserEntity : BaseDomainEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string CodeforcesHandle { get; set; } = string.Empty;
    public string ProfilePicture { get; set; } = string.Empty;
    public string CoverPicture { get; set; } = string.Empty;
    public string Gender { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public string Phone { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Token { get; set; } = "";
    public bool IsVerified { get; set; } = false;
    public string? ConfirmationCode { get; set; }
    public DateTime? ConfirmationCodeExpiration { get; set; }
    
    public int NumberOfProblemsSolved { get; set; }
    public int NumberOfProblemsTaken { get; set; }
    
    public Guid UserTypeId { get; set; }
    public UserTypeEntity UserType { get; set; } = null!;
    
    public Guid GroupId { get; set; }
    public GroupEntity Group { get; set; } = null!;
    
    public List<UserQuestionResultEntity> UserQuestionResults { get; set; } = new List<UserQuestionResultEntity>();
    public List<UserContestResultEntity> UserContestResults { get; set; } = new List<UserContestResultEntity>();
    public List<TeamEntity> Teams { get; set; } = new List<TeamEntity>();
 
}