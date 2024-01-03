
namespace Application.DTOs.User
{
    public class FilteredUserDto : UserResponseDto
    {
        public int NumberOfProblemsSolved { get; set; }
        public int NumberOfProblemsTaken { get; set; }
        public double ContestConversionRate { get; set; }
        public int Rank { get; set; }
    }
}
