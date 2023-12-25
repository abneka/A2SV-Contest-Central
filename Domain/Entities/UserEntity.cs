using Domain.Common;

namespace Domain.Entities;

public class UserEntity : BaseDomainEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string CodeforcesHandle { get; set; } = null!;
    public string Password { get; set; } = null!; 
    public string? Token { get; set; } = "";
    public bool IsVerified { get; set; } = false;
    public string? ConfirmationCode { get; set; }
    public DateTime? ConfirmationCodeExpiration { get; set; }
    
    public Guid UserTypeId { get; set; }
    public UserTypeEntity UserType { get; set; } = null!;
    
    public Guid GroupId { get; set; }
    public GroupEntity Group { get; set; } = null!;
    
    public List<UserQuestionResultEntity> UserQuestionResults { get; set; } = new List<UserQuestionResultEntity>();
    public List<UserContestResultEntity> UserContestResults { get; set; } = new List<UserContestResultEntity>();
    public List<TeamEntity> Teams { get; set; } = new List<TeamEntity>();
 
}