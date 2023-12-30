using System;

namespace Application.Models
{
    public class ContestInfoFromCodeforces
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Phase { get; set; } = string.Empty;
        public bool Frozen { get; set; }
        public int DurationSeconds { get; set; }
        public int StartTimeSeconds { get; set; }
        public int RelativeTimeSeconds { get; set; }
        public string PreparedBy { get; set; } = string.Empty;
        public string WebsiteUrl { get; set; } = string.Empty;
        public int Difficulty { get; set; }
        public string Kind { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Season { get; set; } = string.Empty;
    }
}
