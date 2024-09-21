using Customer.Application.Exceptions;
using Customer.Application.Interfaces;
using Customer.Domain.Repository;

namespace Customer.Application.UseCases.Customer.DeleteAddresses;

public class DeleteAddresses(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork)
    : IDeleteAddresses

{
    public async Task Handle(DeleteAddressesInput request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.Get(
            request.CustomerId,
            cancellationToken);

        var addressesToRemove = customer.Addresses
            .Where(a => request.AddressIds.Contains(a.Id))
            .ToList();

        if (!addressesToRemove.Any())
            throw new NotFoundException(
                $"None of the addresses were found for Customer.");

        customer.RemoveAddress(addressesToRemove.Select(a => a.Id).ToList());

        await customerRepository.Update(customer, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();
    }
}