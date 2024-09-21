using MediatR;

namespace Customer.Application.UseCases.Customer.DeleteAddresses;

public class DeleteAddressesInput(
    List<int> addressIds,
    Guid customerId)
    : IRequest
{
    public Guid CustomerId { get; set; } = customerId;
    public List<int> AddressIds { get; set; } = addressIds;
}