using Customer.Application.UseCases.Customer.Common;
using MediatR;

namespace Customer.Application.UseCases.Customer.UpdateCustomer;

public interface IUpdateCustomer
    : IRequestHandler<UpdateCustomerInput, CustomerModelOutput>
{
}