using MediatR;

namespace Customer.Application.UseCases.Customer.DeleteCustomer;

public interface IDeleteCustomer
    : IRequestHandler<DeleteCustomerInput>
{
}