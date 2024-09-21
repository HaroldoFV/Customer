using Customer.Application.UseCases.Customer.Common;
using MediatR;

namespace Customer.Application.UseCases.Customer.UpdateAddress;

public class UpdateAddressInput
    : IRequest<CustomerModelOutput>
{
    public UpdateAddressInput(
        Guid customerId,
        UpdateAddressRequest address)
    {
        CustomerId = customerId;
        Address = address;
    }

    public Guid CustomerId { get; set; }
    public UpdateAddressRequest Address { get; set; }
}

public class UpdateAddressRequest(
    int addressId,
    string street,
    string number,
    string complement,
    string neighborhood,
    string city,
    string state,
    string zipCode)
{
    public int AddressId { get; set; } = addressId;
    public string Street { get; set; } = street;
    public string Number { get; set; } = number;
    public string Complement { get; set; } = complement;
    public string Neighborhood { get; set; } = neighborhood;
    public string City { get; set; } = city;
    public string State { get; set; } = state;
    public string ZipCode { get; set; } = zipCode;
}