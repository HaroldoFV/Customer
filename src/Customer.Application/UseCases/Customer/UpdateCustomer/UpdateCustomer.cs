using Customer.Application.Interfaces;
using Customer.Application.UseCases.Customer.Common;
using Customer.Domain.Repository;

namespace Customer.Application.UseCases.Customer.UpdateCustomer;

public class UpdateCustomer(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork)
    : IUpdateCustomer
{
    public async Task<CustomerModelOutput> Handle(
        UpdateCustomerInput request,
        CancellationToken cancellationToken)
    {
        var customer = await customerRepository.Get(request.Id, cancellationToken);

        customer.Update(request.Name, request.BirthDate, request.GenderType);

        await customerRepository.Update(customer, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();

        return CustomerModelOutput.FromCustomer(customer);
    }
}