using System.Text.RegularExpressions;
using Customer.Domain.Exception;
using Customer.Domain.Validation;
using Microsoft.EntityFrameworkCore;

namespace Customer.Domain.ValueObject;

[Owned]
public class Address : SeedWork.ValueObject
{
    private static readonly Regex ZipCodeRegex = new Regex(@"^\d{5}-\d{3}$", RegexOptions.Compiled);

    public Address(
        string street,
        string number,
        string complement,
        string neighborhood,
        string city,
        string state,
        string zipCode)
    {
        Street = street;
        Number = number;
        Complement = complement;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        ZipCode = zipCode;

        Validate();
    }

    public int Id { get; private set; }
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Complement { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Street, nameof(Street));
        DomainValidation.MinLength(Street, 3, nameof(Street));

        DomainValidation.NotNullOrEmpty(Number, nameof(Number));
        DomainValidation.NotNullOrEmpty(City, nameof(City));
        DomainValidation.NotNullOrEmpty(State, nameof(State));

        DomainValidation.NotNullOrEmpty(ZipCode, nameof(ZipCode));
        if (!ZipCodeRegex.IsMatch(ZipCode))
        {
            throw new EntityValidationException("Invalid ZipCode format.");
        }

        ValidateNumber();
    }

    private void ValidateNumber()
    {
        if (!int.TryParse(Number, out int parsedNumber))
        {
            throw new EntityValidationException($"Invalid number format for {nameof(Number)}.");
        }

        if (parsedNumber <= 0)
        {
            throw new EntityValidationException($"{nameof(Number)} must be greater than zero.");
        }
    }

    public override bool Equals(SeedWork.ValueObject? other)
    {
        if (other is not Address address)
            return false;

        return Street == address.Street &&
               Number == address.Number &&
               Complement == address.Complement &&
               Neighborhood == address.Neighborhood &&
               City == address.City &&
               State == address.State &&
               ZipCode == address.ZipCode;
    }

    protected override int GetCustomHashCode()
    {
        return HashCode.Combine(Street, Number, Complement, Neighborhood, City, State, ZipCode);
    }
}