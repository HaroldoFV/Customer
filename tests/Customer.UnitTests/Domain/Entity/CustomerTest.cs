using Customer.Domain.Enum;
using Customer.Domain.Exception;
using Customer.Domain.ValueObject;
using FluentAssertions;
using CustomerDomain = Customer.Domain.Entity;

namespace Customer.UnitTests.Domain.Entity;

public class CustomerTest
{
    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Customer - Aggregates")]
    public void Instantiate()
    {
        var datetimeBefore = DateTime.Now;

        var customer = new CustomerDomain.Customer(
            "John Doe",
            new DateTime(1990, 1, 1),
            EGenderType.Male);

        var datetimeAfter = DateTime.Now.AddSeconds(1);

        customer.Should().NotBeNull();
        customer.Name.Should().Be("John Doe");
        customer.BirthDate.Should().Be(new DateTime(1990, 1, 1));
        customer.GenderType.Should().Be(EGenderType.Male);
        customer.Addresses.Should().BeEmpty();
        customer.Id.Should().NotBeEmpty();
        customer.CreatedAt.Should().NotBeSameDateAs(default);
        (customer.CreatedAt >= datetimeBefore).Should().BeTrue();
        (customer.CreatedAt <= datetimeAfter).Should().BeTrue();
    }

    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmptyOrNull))]
    [Trait("Domain", "Customer - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void InstantiateErrorWhenNameIsEmptyOrNull(string? name)
    {
        Action action = () => new CustomerDomain.Customer(
            name!,
            new DateTime(1990, 1, 1),
            EGenderType.Male);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null.");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenBirthDateIsDefault))]
    [Trait("Domain", "Customer - Aggregates")]
    public void InstantiateErrorWhenBirthDateIsDefault()
    {
        Action action = () => new CustomerDomain.Customer(
            "John Doe",
            default,
            EGenderType.Male);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("BirthDate should not be default value.");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenBirthDateIsInFuture))]
    [Trait("Domain", "Customer - Aggregates")]
    public void InstantiateErrorWhenBirthDateIsInFuture()
    {
        Action action = () => new CustomerDomain.Customer(
            "John Doe",
            DateTime.Now.AddDays(1),
            EGenderType.Male);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("BirthDate should not be in the future.");
    }

    [Fact(DisplayName = nameof(InstantiateErrorWhenGenderTypeIsInvalid))]
    [Trait("Domain", "Customer - Aggregates")]
    public void InstantiateErrorWhenGenderTypeIsInvalid()
    {
        Action action = () => new CustomerDomain.Customer(
            "John Doe",
            new DateTime(1990, 1, 1),
            (EGenderType)99);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Invalid GenderType value: 99");
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Customer - Aggregates")]
    public void Update()
    {
        var customer = new CustomerDomain.Customer(
            "John Doe",
            new DateTime(1990, 1, 1),
            EGenderType.Male);

        customer.Update("Jane Doe", new DateTime(1990, 1, 1), EGenderType.Female);

        customer.Name.Should().Be("Jane Doe");
    }

    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmptyOrNull))]
    [Trait("Domain", "Customer - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void UpdateErrorWhenNameIsEmptyOrNull(string? name)
    {
        var customer = new CustomerDomain.Customer(
            "John Doe",
            new DateTime(1990, 1, 1),
            EGenderType.Male);

        Action action = () => customer.Update(name!, new DateTime(1990, 1, 1), EGenderType.Male);

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("Name should not be empty or null.");
    }

    [Fact(DisplayName = nameof(AddAddress))]
    [Trait("Domain", "Customer - Aggregates")]
    public void AddAddress()
    {
        var customer = new CustomerDomain.Customer(
            "John Doe",
            new DateTime(1990, 1, 1),
            EGenderType.Male);

        var address = new Address("Street", "123", "complement", "neighborhood", "City", "State", "01153-000");

        customer.AddAddress(address);

        customer.Addresses.Should().ContainSingle();
        customer.Addresses.First().Should().Be(address);
    }

    [Fact(DisplayName = nameof(RemoveAddress))]
    [Trait("Domain", "Customer - Aggregates")]
    public void RemoveAddress()
    {
        var customer = new CustomerDomain.Customer(
            "John Doe",
            new DateTime(1990, 1, 1),
            EGenderType.Male);

        var address1 = new Address("Main Street", "123", "Apt 1", "Downtown", "City", "State", "01153-000");
        var address2 = new Address("Second Street", "456", "Apt 2", "Suburb", "City", "State", "02000-000");

        // generate private Ids for the addresses
        SetAddressId(address1, 1);
        SetAddressId(address2, 2);

        customer.AddAddress(address1);
        customer.AddAddress(address2);

        var idsToRemove = new List<int> { address1.Id };
        customer.RemoveAddress(idsToRemove);

        customer.Addresses.Should().NotBeEmpty();
        customer.Addresses.Should().ContainSingle();
        customer.Addresses.First().Should().Be(address2);
    }

    private void SetAddressId(Address address, int id)
    {
        var idField = typeof(Address).GetField("<Id>k__BackingField",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (idField == null)
            throw new InvalidOperationException("Field 'Id' not found. Ensure the field name is correct.");

        idField.SetValue(address, id);
    }

    [Theory(DisplayName = nameof(AddInvalidAddressShouldThrowException))]
    [Trait("Domain", "Customer - Aggregates")]
    [InlineData("", "123", "Complement", "Neighborhood", "City", "SP", "12345-678",
        "Street should not be empty or null.")]
    [InlineData("St", "123", "Complement", "Neighborhood", "City", "SP", "12345-678",
        "Street should be at least 3 characters long.")]
    [InlineData("Street", "", "Complement", "Neighborhood", "City", "SP", "12345-678",
        "Number should not be empty or null.")]
    [InlineData("Street", "123", "Complement", "Neighborhood", "", "SP", "12345-678",
        "City should not be empty or null.")]
    [InlineData("Street", "123", "Complement", "Neighborhood", "City", "", "12345-678",
        "State should not be empty or null.")]
    [InlineData("Street", "123", "Complement", "Neighborhood", "City", "SP", "",
        "ZipCode should not be empty or null.")]
    [InlineData("Street", "123", "Complement", "Neighborhood", "City", "SP", "12345678", "Invalid ZipCode format.")]
    public void AddInvalidAddressShouldThrowException(
        string street, string number, string complement, string neighborhood,
        string city, string state, string zipCode, string expectedErrorMessage)
    {
        var customer = new Customer.Domain.Entity.Customer(
            "John Doe",
            new DateTime(1990, 1, 1),
            EGenderType.Male);

        Action action = () => customer.AddAddress(
            new Address(street, number, complement, neighborhood, city, state, zipCode)
        );

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage(expectedErrorMessage);
    }
}