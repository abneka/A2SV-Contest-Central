namespace Domain.Entities.Email;

public class Email
{
    public string To { get; set; } = null!;
    public string? Subject { get; set; }
    public string? Body { get; set; }
}