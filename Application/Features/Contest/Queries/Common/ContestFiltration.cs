using Application.DTOs.Contest;

namespace Application.Features.Contest.Queries.Common;

public static class ContestFiltration
{
    public static IQueryable<ContestResponseDto> FilterByLocation(IQueryable<ContestResponseDto> query,
        string? filterLocation)
    {
        if (!string.IsNullOrEmpty(filterLocation))
        {
            return query.Where(x =>
                x.ContestGroups.Any(cg => cg.Group.Location.Location.ToLower().ToString() == filterLocation.ToLower())
            );
        }

        return query;
    }

    public static IQueryable<ContestResponseDto> FilterByGeneration(IQueryable<ContestResponseDto> query,
        string? filterGeneration)
    {
        if (!string.IsNullOrEmpty(filterGeneration))
        {
            return query.Where(x =>
                x.ContestGroups.Any(cg => cg.Group.Generation.ToLower() == filterGeneration.ToLower())
            );
        }

        return query;
    }

    public static IQueryable<ContestResponseDto> FilterByGroup(IQueryable<ContestResponseDto> query, string? filterGroup)
    {
        if (!string.IsNullOrEmpty(filterGroup))
        {
            filterGroup = filterGroup.ToLower();
            return query.Where(x =>
                x.ContestGroups.Any(cg => cg.Group.Name.ToLower() == filterGroup) ||
                x.ContestGroups.Any(cg => cg.Group.Abbreviation.ToLower() == filterGroup)
            );
        }

        return query;
    }

    public static IQueryable<ContestResponseDto> FilterByCountry(IQueryable<ContestResponseDto> query, string? filterCountry)
    {
        if (!string.IsNullOrEmpty(filterCountry))
        {
            return query.Where(x =>
                x.ContestGroups.Any(cg => cg.Group.Location.Country.ToLower() == filterCountry.ToLower())
            );
        }

        return query;
    }

    public static IQueryable<ContestResponseDto> FilterByAny(IQueryable<ContestResponseDto> query, string? searchString)
    {
        if (!string.IsNullOrEmpty(searchString))
        {
            searchString = searchString.ToLower();
            return query.Where(x =>
                x.Name.ToLower().Contains(searchString) ||
                x.Description.ToLower().Contains(searchString) ||
                x.Status.ToLower().Contains(searchString) ||
                x.PreparedBy.ToLower().Contains(searchString)
            );
        }

        return query;
    }
}