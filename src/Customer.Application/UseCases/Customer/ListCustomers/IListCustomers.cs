using MediatR;

namespace Customer.Application.UseCases.Customer.ListCustomers;

public interface IListCustomers
    : IRequestHandler<ListCustomersInput, ListCustomersOutput>
{
}