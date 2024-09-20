using System.Collections.ObjectModel;
using Customer.Domain.Enum;
using Customer.Domain.Exception;
using Customer.Domain.Validation;
using Customer.Domain.ValueObject;

namespace Customer.Domain.Entity;

public class Customer : SeedWork.Entity
{
    public string Name { get; private set; }
    public DateTime BirthDate { get; private set; }
    public EGenderType GenderType { get; private set; }
    public IList<Address> Addresses { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Customer(
        string name,
        DateTime birthDate,
        EGenderType genderType)
    {
        Name = name;
        BirthDate = birthDate;
        GenderType = genderType;
        Addresses = new Collection<Address>();
        CreatedAt = DateTime.Now;

        Validate();
    }

    public void Update(string name)
    {
        Name = name;

        Validate();
    }

    public void AddAddress(Address address)
    {
        Addresses.Add(address);

        Validate();
    }

    public void RemoveAddress(Address address)
    {
        Addresses.Remove(address);

        Validate();
    }

    public void Validate()
    {
        DomainValidation.NotNullOrEmpty(Name, nameof(Name));
        if (BirthDate == default)
            throw new EntityValidationException("BirthDate should not be default value.");

        if (BirthDate > DateTime.Now)
            throw new EntityValidationException("BirthDate should not be in the future.");

        if (!System.Enum.IsDefined(typeof(EGenderType), GenderType))
            throw new EntityValidationException($"Invalid GenderType value: {GenderType}");
    }
}