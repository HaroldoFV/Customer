using Customer.Application.UseCases.Customer.Common;
using Customer.Domain.Repository;

namespace Customer.Application.UseCases.Customer.ListCustomers;

public class ListCustomers(
    ICustomerRepository customerRepository)
    : IListCustomers
{
    public async Task<ListCustomersOutput> Handle(
        ListCustomersInput request,
        CancellationToken cancellationToken)
    {
        var searchOutput = await customerRepository.Search(
            new(
                request.Page,
                request.PerPage,
                request.Search,
                request.Sort,
                request.Dir
            ),
            cancellationToken
        );

        return new ListCustomersOutput(
            searchOutput.CurrentPage,
            searchOutput.PerPage,
            searchOutput.Total,
            searchOutput.Items
                .Select(CustomerModelOutput.FromCustomer)
                .ToList()
        );
    }
}