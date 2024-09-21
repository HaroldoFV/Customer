using MediatR;

namespace Customer.Application.UseCases.Customer.DeleteCustomer;

public class DeleteCustomerInput(Guid id) : IRequest
{
    public Guid Id { get; set; } = id;
}