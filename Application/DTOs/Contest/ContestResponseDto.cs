
using Application.DTOs.Common;

namespace Application.DTOs.Contest
{
    public class ContestResponseDto : BaseDto
    {
        public string ContestGlobalId { get; set; } = string.Empty;
        public string ContestUrl { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int DurationSeconds { get; set; }
        public int StartTimeSeconds { get; set; }
        public int RelativeTimeSeconds { get; set; }
        public string PreparedBy { get; set; } = string.Empty;
        public string WebsiteUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public string Kind { get; set; } = string.Empty;
        public string Season { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        
    }
}