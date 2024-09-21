using Customer.Application.UseCases.Customer.Common;
using Customer.Domain.Enum;
using MediatR;

namespace Customer.Application.UseCases.Customer.CreateCustomer;

public class CreateCustomerInput : IRequest<CustomerModelOutput>
{
    public required string Name { get; set; }
    public required DateTime BirthDate { get; set; }
    public required EGenderType GenderType { get; set; }
    public required IList<AddressModelInput> Addresses { get; set; }
}