using Customer.Application.UseCases.Customer.Common;
using MediatR;

namespace Customer.Application.UseCases.Customer.AddAddress;

public class AddAddressInput(
    Guid customerId,
    List<AddAddressModelInput> addressModelInputs
) : IRequest<CustomerModelOutput>
{
    public Guid CustomerId { get; set; } = customerId;
    public List<AddAddressModelInput> AddressModelInputs { get; set; } = addressModelInputs;
}

public class AddAddressModelInput(
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
}