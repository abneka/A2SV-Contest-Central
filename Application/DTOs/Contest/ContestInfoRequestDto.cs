namespace Application.DTOs.Contest
{
    public class ContestInfoRequestDto
    {
        public string ContestName { get; set; } = null!;
        public string ContestUrl { get; set; } = null!;
        public int DurationSeconds { get; set; }
        public int StartTimeSeconds { get; set; }
        public string PreparedBy { get; set; } = string.Empty;
    }
}
