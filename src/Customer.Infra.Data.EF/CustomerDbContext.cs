using Customer.Infra.Data.EF.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Customer.Infra.Data.EF;

public class CustomerDbContext(
    DbContextOptions<CustomerDbContext> options
) : DbContext(options)
{
    public DbSet<Domain.Entity.Customer> Customers => Set<Domain.Entity.Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        return base.SaveChangesAsync(cancellationToken);
    }
}