using Customer.Domain.ValueObject;

namespace Customer.Application.UseCases.Customer.Common;

public class AddressModelInput(
    string street,
    string number,
    string complement,
    string neighborhood,
    string city,
    string state,
    string zipCode)
{
    public string Street { get; set; } = street;
    public string Number { get; set; } = number;
    public string Complement { get; set; } = complement;
    public string Neighborhood { get; set; } = neighborhood;
    public string City { get; set; } = city;
    public string State { get; set; } = state;
    public string ZipCode { get; set; } = zipCode;

    public static AddressModelInput FromAddress(Address address)
    {
        return new AddressModelInput(
            address.Street,
            address.Number,
            address.Complement,
            address.Neighborhood,
            address.City,
            address.State,
            address.ZipCode
        );
    }
}