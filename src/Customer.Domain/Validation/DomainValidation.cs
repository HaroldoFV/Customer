using Customer.Domain.Exception;

namespace Customer.Domain.Validation;

public static class DomainValidation
{
    public static void NotNullOrEmpty(string? target, string fieldName)
    {
        if (String.IsNullOrEmpty(target))
            throw new EntityValidationException(
                $"{fieldName} should not be empty or null.");

        if (String.IsNullOrWhiteSpace(target))
            throw new EntityValidationException(
                $"{fieldName} should not be empty or null.");
    }

    public static void MinLength(string target, int minLength, string fieldName)
    {
        if (target.Length < minLength)
            throw new EntityValidationException($"{fieldName} should be at least {minLength} characters long.");
    }
}