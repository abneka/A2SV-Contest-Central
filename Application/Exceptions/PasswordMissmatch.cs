namespace Application.Exceptions;

public class PasswordMismatch: Exception
{
    public PasswordMismatch(string message) : base(message)
    {

    }
    
}