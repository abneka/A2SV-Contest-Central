

namespace Application.DTOs.Contest
{
    public class ContestRequestDto
    {
        public string ContestName { get; set; } = null!;
        public string ContestUrl { get; set; } = null!;
        public IReadOnlyList<string> Questions { get; set; } = null!;
        public IReadOnlyList<string> Groups { get; set; } = null!;
        public IReadOnlyList<string>? Universities { get; set; }
        public IReadOnlyList<string>? Countries { get; set; }
    }
} 

