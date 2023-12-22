using Application.Contracts.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Contest.Commands.UpdateContest;

public class UpdateContestCommandValidator : AbstractValidator<UpdateContestCommand>
{
    public IUserRepository _userRepository {get; set;}
    
    public UpdateContestCommandValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;
      
    }

}