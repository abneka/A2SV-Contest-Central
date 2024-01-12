namespace Application.DTOs.User
{
    public class InvalidUserRecord
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email{ get; set; } = string.Empty;
        public string Username{ get; set; } = string.Empty;
        public string CodeforcesHandle{ get; set; } = string.Empty;
        public string Phone{ get; set; } = string.Empty;
        public string Gender{ get; set; } = string.Empty;
        public string Password{ get; set; } = string.Empty;
        public string Role{ get; set; } = string.Empty;
        public string Group{ get; set; } = string.Empty;
    }
}
