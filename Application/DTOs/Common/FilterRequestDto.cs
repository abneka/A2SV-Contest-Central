namespace Application.DTOs.Common
{
    public class FilterRequestDto
    {
        public string? SearchString { get; set; }
        public string? Country { get; set; }
        public string? Generation { get; set; }
        public string? Location { get; set; }
        public string? Group { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
