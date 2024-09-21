using Customer.Application.Exceptions;
using Customer.Domain.Repository;
using Customer.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;
using DomainEntity = Customer.Domain.Entity;

namespace Customer.Infra.Data.EF.Repository;

public class CustomerRepository(CustomerDbContext context)
    : ICustomerRepository
{
    private DbSet<Domain.Entity.Customer> Customers => context.Set<Domain.Entity.Customer>();

    public async Task Insert(Domain.Entity.Customer aggregate, CancellationToken cancellationToken)
    {
        await Customers.AddAsync(aggregate, cancellationToken);
    }

    public async Task<Domain.Entity.Customer> Get(Guid id, CancellationToken cancellationToken)
    {
        var customer = await Customers
            .Include(c => c.Addresses)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken
            );
        NotFoundException.ThrowIfNull(customer, $"Customer '{id}' not found.");

        return customer!;
    }

    public Task Delete(Domain.Entity.Customer aggregate, CancellationToken cancellationToken)
    {
        return Task.FromResult(Customers.Remove(aggregate));
    }

    public Task Update(Domain.Entity.Customer aggregate, CancellationToken cancellationToken)
    {
        return Task.FromResult(Customers.Update(aggregate));
    }

    public async Task<SearchOutput<DomainEntity.Customer>> Search(SearchInput input,
        CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = Customers.AsNoTracking();
        query = AddOrderToQuery(query, input.OrderBy, input.Order);

        if (!String.IsNullOrWhiteSpace(input.Search))
            query = query.Where(x => x.Name.ToLower().Contains(input.Search.ToLower()));
        var total = await query.CountAsync(cancellationToken: cancellationToken);

        var items = await query
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync(cancellationToken: cancellationToken);

        return new(input.Page, input.PerPage, total, items);
    }

    private IQueryable<DomainEntity.Customer> AddOrderToQuery(
        IQueryable<DomainEntity.Customer> query,
        string orderProperty,
        SearchOrder order
    )
    {
        var orderedQuery = (orderProperty.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name)
                .ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name)
                .ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
            ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
            ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
            _ => query.OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
        };
        return orderedQuery;
    }
}