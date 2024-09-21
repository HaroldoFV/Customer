using Customer.Application.Interfaces;
using Customer.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infra.Data.EF;

public class UnitOfWork(
    CustomerDbContext context
)
    : IUnitOfWork
{
    public async Task Commit(CancellationToken cancellationToken)
    {
        var aggregateRoots = context.ChangeTracker
            .Entries<AggregateRoot>()
            .Select(entry => entry.Entity);

        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("An error occurred while saving changes.", ex.InnerException);
        }
    }

    public Task Rollback(CancellationToken cancellationToken)
        => Task.CompletedTask;
}