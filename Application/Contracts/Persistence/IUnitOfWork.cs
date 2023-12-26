using Domain.Entities;

namespace Application.Contracts.Persistence;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IUserTypeRepository UserTypeRepository { get; }
    ITeamRepository TeamRepository { get; }
    IContestRepository ContestRepository { get; }
    ILocationRepository LocationRepository { get; }
    IA2SVGroupRepository A2SVGroupRepository { get; }
    IUserContestResultRepository UserContestResultRepository { get; }
}