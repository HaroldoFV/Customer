using Customer.Domain.ValueObject;

namespace Customer.Application.UseCases.Customer.Common;

public class AddressModelOutput(
    int id,
    string street,
    string number,
    string complement,
    string neighborhood,
    string city,
    string state,
    string zipCode)
{
    public int Id { get; set; } = id;
    public string Street { get; set; } = street;
    public string Number { get; set; } = number;
    public string Complement { get; set; } = complement;
    public string Neighborhood { get; set; } = neighborhood;
    public string City { get; set; } = city;
    public string State { get; set; } = state;
    public string ZipCode { get; set; } = zipCode;

    public static AddressModelOutput FromAddress(Address address)
    {
        return new AddressModelOutput(
            address.Id,
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