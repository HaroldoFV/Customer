using Customer.Application.UseCases.Customer.Common;
using MediatR;

namespace Customer.Application.UseCases.Customer.AddAddress;

public interface IAddAddresses
    : IRequestHandler<AddAddressInput, CustomerModelOutput>
{
}