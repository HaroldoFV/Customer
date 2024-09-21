using Customer.Application.UseCases.Customer.Common;
using MediatR;

namespace Customer.Application.UseCases.Customer.GetCustomer;

public interface IGetCustomer :
    IRequestHandler<GetCustomerInput, CustomerModelOutput>
{
}