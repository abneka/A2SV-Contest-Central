namespace Application.DTOs.Common
{
    public class PaginatedResult<T> where T : class
    {
        public IReadOnlyList<T>? Data { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
}
