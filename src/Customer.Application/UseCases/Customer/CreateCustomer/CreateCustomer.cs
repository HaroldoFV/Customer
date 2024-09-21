using Customer.Application.Interfaces;
using Customer.Application.UseCases.Customer.Common;
using Customer.Domain.Repository;
using Customer.Domain.ValueObject;
using DomainEntity = Customer.Domain.Entity;

namespace Customer.Application.UseCases.Customer.CreateCustomer;

public class CreateCustomer(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork)
    : ICreateCustomer
{
    public async Task<CustomerModelOutput> Handle(
        CreateCustomerInput request,
        CancellationToken cancellationToken)
    {
        var customer = new DomainEntity.Customer(
            request.Name,
            request.BirthDate,
            request.GenderType
        );

        foreach (var address in request.Addresses)
        {
            customer.AddAddress(new Address(
                address.Street,
                address.Number,
                address.Complement,
                address.Neighborhood,
                address.City,
                address.State,
                address.ZipCode
            ));
        }

        await customerRepository.Insert(customer, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();

        return CustomerModelOutput.FromCustomer(customer);
    }
}