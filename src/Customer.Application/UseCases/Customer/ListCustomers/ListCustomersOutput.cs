using Customer.Application.Common;
using Customer.Application.UseCases.Customer.Common;

namespace Customer.Application.UseCases.Customer.ListCustomers;

public class ListCustomersOutput(
    int page,
    int perPage,
    int total,
    IReadOnlyList<CustomerModelOutput> items)
    : PaginatedListOutput<CustomerModelOutput>(page, perPage, total, items);