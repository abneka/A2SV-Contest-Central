

namespace Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message, object key) : base($"{message} {key} was not found")
    {

    }
}