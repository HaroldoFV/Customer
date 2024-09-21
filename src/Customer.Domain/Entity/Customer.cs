using Customer.Domain.Enum;
using Customer.Domain.Exception;
using Customer.Domain.SeedWork;
using Customer.Domain.Validation;
using Customer.Domain.ValueObject;

namespace Customer.Domain.Entity;

public class Customer : AggregateRoot
{
    public string Name { get; private set; }
    public DateTime BirthDate { get; private set; }
    public EGenderType GenderType { get; private set; }
    private readonly List<Address> _addresses = new List<Address>();
    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();
    public DateTime CreatedAt { get; private set; }

    public Customer(
        string name,
        DateTime birthDate,
        EGenderType genderType)
    {
        Name = name;
        BirthDate = birthDate;
        GenderType = genderType;
        _addresses = new List<Address>();

        CreatedAt = DateTime.Now;

        Validate();
    }

    public void Update(string name, DateTime? birthDate, EGenderType? genderType)
    {
        Name = name;
        BirthDate = birthDate ?? BirthDate;
        GenderType = genderType ?? GenderType;

        Validate();
    }

    public void AddAddress(Address address)
    {
        _addresses.Add(address);
    }

    public void UpdateAddress(int addressId, Address newAddress)
    {
        var oldAddress = _addresses.FirstOrDefault(a => a.Id == addressId);

        if (oldAddress == null)
            throw new EntityValidationException($"Address with ID '{addressId}' not found.");

        _addresses.Remove(oldAddress);
        _addresses.Add(newAddress);

        Validate();
    }

    public void RemoveAddress(List<int> addressIds)
    {
        if (addressIds == null || addressIds.Count == 0)
            throw new EntityValidationException("AddressIds should not be null or empty.");

        _addresses.RemoveAll(a => addressIds.Contains(a.Id));

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