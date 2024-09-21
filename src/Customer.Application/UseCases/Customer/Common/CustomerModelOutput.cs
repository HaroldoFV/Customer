using Customer.Domain.Enum;
using DomainEntity = Customer.Domain.Entity;

namespace Customer.Application.UseCases.Customer.Common;

public class CustomerModelOutput(
    Guid id,
    string name,
    DateTime birthDate,
    EGenderType genderType,
    IList<AddressModelOutput> addresses,
    DateTime createdAt)
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public DateTime BirthDate { get; private set; } = birthDate;
    public EGenderType GenderType { get; private set; } = genderType;
    public IList<AddressModelOutput> Addresses { get; private set; } = addresses;
    public DateTime CreatedAt { get; private set; } = createdAt;

    public static CustomerModelOutput FromCustomer(DomainEntity.Customer customer)
    {
        return new CustomerModelOutput(
            customer.Id,
            customer.Name,
            customer.BirthDate,
            customer.GenderType,
            customer.Addresses.Select(AddressModelOutput.FromAddress).ToList(),
            customer.CreatedAt
        );
    }
}