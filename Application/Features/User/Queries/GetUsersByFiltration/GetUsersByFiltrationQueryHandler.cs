using Application.Contracts.Persistence;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.User.Queries.GetUsersByFiltration;

public class GetUsersByFiltrationQueryHandler : IRequestHandler<GetUsersByFiltrationQuery, PaginatedUserResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUsersByFiltrationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginatedUserResponseDto> Handle(GetUsersByFiltrationQuery request, CancellationToken cancellationToken)
    {
        var skip = (request.Filter.PageNumber - 1) * request.Filter.PageSize;
        var result = await _unitOfWork.UserRepository.GetAllAsync();
        var users = _mapper.Map<List<UserDto>>(result);
        var orderedUsers = users.OrderBy(u => u.ContestConversionRate).ToList();

        for (int i = 1; i <= orderedUsers.Count; i++)
        {
            orderedUsers[i - 1].Rank = i;
        }
        
        var query = FilterByAny(orderedUsers.AsQueryable(), request.Filter.SearchString);
        query = FilterByCountry(query, request.Filter.Country);
        query = FilterByGroup(query, request.Filter.Group);
        query = FilterByGeneration(query, request.Filter.Generation);
        query = FilterByLocation(query, request.Filter.Location);

        query = query.Skip(skip).Take(request.Filter.PageSize);
        
        
        return new PaginatedUserResponseDto
            { UsersList = query.ToList(), ItemsCount = query.Count(), PageNumber = request.Filter.PageNumber };
    }
    
     private IQueryable<UserDto> FilterByLocation(IQueryable<UserDto> query, string? filterLocation)
        {
            if (!string.IsNullOrEmpty(filterLocation))
            {
                return query.Where(x =>
                    x.Group.Location.Location.ToLower() == filterLocation.ToLower());
            }

            return query;
        }

        private IQueryable<UserDto> FilterByGeneration(IQueryable<UserDto> query, string? filterGeneration)
        {
            if (!string.IsNullOrEmpty(filterGeneration))
            {
                return query.Where(x =>
                    x.Group.Generation.ToLower() == filterGeneration.ToLower()
                );
            }

            return query;
        }

        private IQueryable<UserDto> FilterByGroup(IQueryable<UserDto> query, string? filterGroup)
        {
            if (!string.IsNullOrEmpty(filterGroup))
            {
                return query.Where(x => 
                    x.Group.Name.ToLower().Contains(filterGroup.ToLower()) ||
                    x.Group.Abbreviation.ToLower().Contains(filterGroup.ToLower())
                );
            }

            return query;
        }

        private IQueryable<UserDto> FilterByCountry(IQueryable<UserDto> query, string? filterCountry)
        {
            if (!string.IsNullOrEmpty(filterCountry))
            {
                return query.Where(x => 
                    x.Group.Location.Country.ToLower() == filterCountry.ToLower()
                );
            }

            return query;
        }

        private IQueryable<UserDto> FilterByAny(IQueryable<UserDto> query, string? searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                return query.Where(x =>
                   x.FirstName.ToLower().Contains(searchString.ToLower()) || 
                   x.LastName.ToLower().Contains(searchString.ToLower()) ||
                   x.Email.ToLower().Contains(searchString.ToLower()) || 
                   x.UserName.ToLower().Contains(searchString.ToLower()) ||
                   x.CodeforcesHandle.ToLower().Contains(searchString.ToLower())
                );
            }

            return query;
        }
}