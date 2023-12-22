using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.Repositories.Common;

namespace Persistence.Repositories;

public class LocationRepository : GenericRepository<LocationEntity>, ILocationRepository
{
    protected LocationRepository(AppDBContext dbContext) : base(dbContext)
    {
    }
}