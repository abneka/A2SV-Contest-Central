using Application.Contracts.Persistence.Common;
using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface ILocationRepository : IGenericRepository<LocationEntity>
{
    Task<List<LocationEntity>> GetByName(string requestLocationName);
}