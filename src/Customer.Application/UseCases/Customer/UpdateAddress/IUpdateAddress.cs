using Customer.Application.UseCases.Customer.Common;
using MediatR;

namespace Customer.Application.UseCases.Customer.UpdateAddress;

public interface IUpdateAddress
    : IRequestHandler<UpdateAddressInput, CustomerModelOutput>
{
}