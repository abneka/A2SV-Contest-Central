using Application.Contracts.Persistence;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Contest.Commands.UpdateContest;

public class UpdateContestCommandValidator : AbstractValidator<UpdateContestCommand>
{    
    public UpdateContestCommandValidator()
    {
        //groups  questions url  name
      
    }
}