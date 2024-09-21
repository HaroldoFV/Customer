namespace Customer.Api.ApiModels.Customer;

public class AddAddressApiInput(
    List<AddressModelApiInput> addressModelInputs)
{
    public List<AddressModelApiInput> AddressModelInputs { get; set; } = addressModelInputs;
}

public class AddressModelApiInput(
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