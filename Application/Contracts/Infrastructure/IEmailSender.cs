using Domain.Entities.Email;

namespace Application.Contracts.Infrastructure;

public interface IEmailSender
{
    public Task SendEmail(Email email);
}