using Customer.Application.UseCases.Customer.Common;
using Customer.Domain.Repository;

namespace Customer.Application.UseCases.Customer.GetCustomer;

public class GetCustomer(
    ICustomerRepository customerRepository)
    : IGetCustomer
{
    public async Task<CustomerModelOutput> Handle(
        GetCustomerInput request,
        CancellationToken cancellationToken)
    {
        var customer = await customerRepository.Get(request.Id, cancellationToken);
        return CustomerModelOutput.FromCustomer(customer);
    }
}