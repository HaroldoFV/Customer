using Customer.Application.Exceptions;
using Customer.Application.Interfaces;
using Customer.Application.UseCases.Customer.Common;
using Customer.Domain.Repository;
using Customer.Domain.ValueObject;

namespace Customer.Application.UseCases.Customer.UpdateAddress;

public class UpdateAddress(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork)
    : IUpdateAddress
{
    public async Task<CustomerModelOutput> Handle(UpdateAddressInput request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.Get(request.CustomerId, cancellationToken);

        var address = customer.Addresses.FirstOrDefault(a => a.Id == request.Address.AddressId);
        if (address is null) throw new NotFoundException("Address not found.");

        var newAddress = new Address(
            request.Address.Street,
            request.Address.Number,
            request.Address.Complement,
            request.Address.Neighborhood,
            request.Address.City,
            request.Address.State,
            request.Address.ZipCode
        );

        customer.UpdateAddress(request.Address.AddressId, newAddress);

        await customerRepository.Update(customer, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();

        return CustomerModelOutput.FromCustomer(customer);
    }
}