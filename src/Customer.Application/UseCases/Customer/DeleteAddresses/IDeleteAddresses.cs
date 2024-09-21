using MediatR;

namespace Customer.Application.UseCases.Customer.DeleteAddresses;

public interface IDeleteAddresses
    : IRequestHandler<DeleteAddressesInput>
{
}