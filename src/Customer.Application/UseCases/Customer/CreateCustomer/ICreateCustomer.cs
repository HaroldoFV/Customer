using Customer.Application.UseCases.Customer.Common;
using MediatR;

namespace Customer.Application.UseCases.Customer.CreateCustomer;

public interface ICreateCustomer
    : IRequestHandler<CreateCustomerInput, CustomerModelOutput>
{
}