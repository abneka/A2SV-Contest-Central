namespace Application.DTOs.User;

public class PaginatedUserResponseDto
{
    public List<FilteredUserDto> UsersList { get; set; }
    public int ItemsCount { get; set; }
    public int PageNumber { get; set; }

}