using Customer.Domain.Enum;

namespace Customer.Api.ApiModels.Customer;

public class UpdateCustomerApiInput
{
    public required string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public EGenderType GenderType { get; set; }
}