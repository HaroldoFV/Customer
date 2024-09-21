using Customer.Application.UseCases.Customer.Common;
using Customer.Domain.Enum;
using MediatR;

namespace Customer.Application.UseCases.Customer.UpdateCustomer;

public class UpdateCustomerInput(
    Guid id,
    string name,
    DateTime birthDate,
    EGenderType genderType
)
    : IRequest<CustomerModelOutput>
{
    public Guid Id { get; set; } = id;
    public string Name { get; set; } = name;
    public DateTime BirthDate { get; set; } = birthDate;
    public EGenderType GenderType { get; set; } = genderType;
}