
using Application.DTOs.Common;

namespace Application.DTOs.Contest
{
    public class ContestResponseDto : BaseDto
    {
        public string ContestGlobalId { get; set; } = null!;
        public string ContestUrl { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public DateTime StartTime { get; set; }
        public TimeSpan RelativeTime { get; set; }
        public string PreparedBy { get; set; } = string.Empty;
        public string WebsiteUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Difficulty { get; set; } = string.Empty;
        public string Kind { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Season { get; set; } = string.Empty;
        
    }
}