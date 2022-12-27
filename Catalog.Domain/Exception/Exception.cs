namespace Catalog.Domain.Exception;

public class DomainException : System.Exception
{
    public DomainException()
    {
    }

    public DomainException(string message) : base(message)
    {
    }

    public DomainException(string message, System.Exception innerException) : base(message, innerException)
    {
    }
}