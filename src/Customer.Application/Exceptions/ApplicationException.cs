namespace Customer.Application.Exceptions;

public abstract class ApplicationException(string? message)
    : Exception(message);