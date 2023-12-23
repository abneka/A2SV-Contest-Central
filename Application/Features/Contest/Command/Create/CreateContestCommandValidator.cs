
using Application.Contracts.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Contest.Commands.CreateContest;

public class CreateContestCommandValidator : AbstractValidator<CreateContestCommand>
{
    public CreateContestCommandValidator(IUserRepository userRepository)
    {
    
        
    }
}