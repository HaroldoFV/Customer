using Customer.Application.UseCases.Customer.Common;
using MediatR;

namespace Customer.Application.UseCases.Customer.GetCustomer;

public class GetCustomerInput(Guid id)
    : IRequest<CustomerModelOutput>
{
    public Guid Id { get; set; } = id;
}