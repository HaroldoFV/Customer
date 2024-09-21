using Customer.Application.Common;
using Customer.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace Customer.Application.UseCases.Customer.ListCustomers;

public class ListCustomersInput(
    int page = 1,
    int perPage = 15,
    string search = "",
    string sort = "",
    SearchOrder dir = SearchOrder.Asc)
    : PaginatedListInput(page, perPage, search, sort, dir), IRequest<ListCustomersOutput>
{
    public ListCustomersInput()
        : this(1, 15, "", "", SearchOrder.Asc)
    {
    }
}