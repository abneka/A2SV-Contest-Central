using FluentValidation;

namespace Application.Features.Team.Commands;

public class CreateTeamCommandValidator : AbstractValidator<CreateTeamCommand>
{
    public CreateTeamCommandValidator()
    {
        
    }
}