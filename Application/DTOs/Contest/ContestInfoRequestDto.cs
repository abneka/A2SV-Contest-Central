namespace Application.DTOs.Contest
{
    public class ContestInfoRequestDto
    {
        public string ContestName { get; set; } = null!;
        public string ContestUrl { get; set; } = null!;
        public string Duration { get; set; } = string.Empty;
        public string StartDay { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string PreparedBy { get; set; } = string.Empty;
    }
}
