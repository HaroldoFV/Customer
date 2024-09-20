using FluentAssertions;
using Customer.Domain.ValueObject;
using Customer.Domain.Exception;

namespace Customer.UnitTests.Domain.ValueObject
{
    public class AddressTest
    {
        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Address - ValueObject")]
        public void Instantiate()
        {
            var address = new Address(
                "Main Street",
                "123",
                "Apt 4",
                "Downtown",
                "New York",
                "NY",
                "12345-678");

            address.Street.Should().Be("Main Street");
            address.Number.Should().Be("123");
            address.Complement.Should().Be("Apt 4");
            address.Neighborhood.Should().Be("Downtown");
            address.City.Should().Be("New York");
            address.State.Should().Be("NY");
            address.ZipCode.Should().Be("12345-678");
        }

        [Theory(DisplayName = nameof(ThrowWhenInvalid))]
        [Trait("Domain", "Address - ValueObject")]
        [InlineData("", "123", "Complement", "Neighborhood", "City", "ST", "12345-678",
            "Street should not be empty or null.")]
        [InlineData("St", "123", "Complement", "Neighborhood", "City", "ST", "12345-678",
            "Street should be at least 3 characters long.")]
        [InlineData("Street", "", "Complement", "Neighborhood", "City", "ST", "12345-678",
            "Number should not be empty or null.")]
        [InlineData("Street", "123", "Complement", "Neighborhood", "", "ST", "12345-678",
            "City should not be empty or null.")]
        [InlineData("Street", "123", "Complement", "Neighborhood", "City", "", "12345-678",
            "State should not be empty or null.")]
        [InlineData("Street", "123", "Complement", "Neighborhood", "City", "ST", "",
            "ZipCode should not be empty or null.")]
        [InlineData("Street", "123", "Complement", "Neighborhood", "City", "ST", "12345678", "Invalid ZipCode format.")]
        public void ThrowWhenInvalid(
            string street, string number, string complement, string neighborhood,
            string city, string state, string zipCode, string expectedExceptionMessage)
        {
            Action action = () => new Address(street, number, complement, neighborhood, city, state, zipCode);

            action.Should().Throw<EntityValidationException>()
                .WithMessage(expectedExceptionMessage);
        }

        [Fact(DisplayName = nameof(Equals_ReturnsTrueForEqualAddresses))]
        [Trait("Domain", "Address - ValueObject")]
        public void Equals_ReturnsTrueForEqualAddresses()
        {
            var address1 = new Address("Main St", "123", "Apt 4", "Downtown", "New York", "NY", "12345-678");
            var address2 = new Address("Main St", "123", "Apt 4", "Downtown", "New York", "NY", "12345-678");

            address1.Equals(address2).Should().BeTrue();
        }

        [Fact(DisplayName = nameof(Equals_ReturnsFalseForDifferentAddresses))]
        [Trait("Domain", "Address - ValueObject")]
        public void Equals_ReturnsFalseForDifferentAddresses()
        {
            var address1 = new Address("Main St", "123", "Apt 4", "Downtown", "New York", "NY", "12345-678");
            var address2 = new Address("Broadway", "456", "Suite 7", "Midtown", "New York", "NY", "87654-321");

            address1.Equals(address2).Should().BeFalse();
        }

        [Fact(DisplayName = nameof(GetHashCode_ReturnsSameValueForEqualAddresses))]
        [Trait("Domain", "Address - ValueObject")]
        public void GetHashCode_ReturnsSameValueForEqualAddresses()
        {
            var address1 = new Address("Main St", "123", "Apt 4", "Downtown", "New York", "NY", "12345-678");
            var address2 = new Address("Main St", "123", "Apt 4", "Downtown", "New York", "NY", "12345-678");

            address1.GetHashCode().Should().Be(address2.GetHashCode());
        }
    }
}