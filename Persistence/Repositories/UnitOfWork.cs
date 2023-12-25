using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{

    public IUserRepository UserRepository { get; }
    public IUserTypeRepository UserTypeRepository { get; }
    public ITeamRepository TeamRepository { get; }
    public IContestRepository ContestRepository { get; }
    public ILocationRepository LocationRepository { get; }
    public IA2SVGroupRepository A2SVGroupRepository { get; }
    
    private readonly AppDBContext _dbContext;
    
    public UnitOfWork(IUserRepository userRepository, IUserTypeRepository userTypeRepository, ITeamRepository teamRepository, IContestRepository contestRepository, ILocationRepository locationRepository, IA2SVGroupRepository a2SvGroupRepository, AppDBContext dbContext)
    {
        UserRepository = userRepository;
        UserTypeRepository = userTypeRepository;
        TeamRepository = teamRepository;
        ContestRepository = contestRepository;
        LocationRepository = locationRepository;
        A2SVGroupRepository = a2SvGroupRepository;
        _dbContext = dbContext;
    }
    
}
