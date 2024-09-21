using Customer.Application.Interfaces;
using Customer.Domain.Repository;

namespace Customer.Application.UseCases.Customer.DeleteCustomer;

public class DeleteCustomer(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork)
    : IDeleteCustomer
{
    public async Task Handle(
        DeleteCustomerInput request,
        CancellationToken cancellationToken)
    {
        var customer = await customerRepository.Get(
            request.Id,
            cancellationToken);

        await customerRepository.Delete(customer, cancellationToken);
        await unitOfWork.Commit(cancellationToken);

        cancellationToken.ThrowIfCancellationRequested();
    }
}