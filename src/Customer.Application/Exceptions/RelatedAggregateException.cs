namespace Customer.Application.Exceptions;

public class RelatedAggregateException : System.ApplicationException
{
    public RelatedAggregateException(string? message) : base(message)
    {
    }
}