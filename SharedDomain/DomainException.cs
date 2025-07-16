namespace SharedDomain;

public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
    }
}