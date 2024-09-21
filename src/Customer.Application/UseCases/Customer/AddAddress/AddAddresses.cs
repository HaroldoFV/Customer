using Customer.Application.Interfaces;
using Customer.Application.UseCases.Customer.Common;
using Customer.Domain.Repository;
using Customer.Domain.ValueObject;

namespace Customer.Application.UseCases.Customer.AddAddress;

public class AddAddresses(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork)
    : IAddAddresses
{
    public async Task<CustomerModelOutput> Handle(
        AddAddressInput request,
        CancellationToken cancellationToken)
    {
        var customer = await customerRepository.Get(request.CustomerId, cancellationToken);

        foreach (var addressDto in request.AddressModelInputs)
        {
            var newAddress = new Address(
                addressDto.Street,
                addressDto.Number,
                addressDto.Complement,
                addressDto.Neighborhood,
                addressDto.City,
                addressDto.State,
                addressDto.ZipCode
            );

            customer.AddAddress(newAddress);
        }

        await customerRepository.Update(customer, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();

        return CustomerModelOutput.FromCustomer(customer);
    }
}