namespace Application.DTOs.Contest.CodeforcesExtension
{
    public class ContestExtRequestDto
    {
        public string ContestId { get; set; } = null!;
        public string ContestName { get; set; } = null!;
        public string ContestUrl { get; set; } = null!;
        public string Duration { get; set; } = null!;
        public string StartDay { get; set; } = null!;
        public string StartTime { get; set; } = null!;
        public string PreparedBy { get; set; } = null!;
    }
}
