using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customer.Infra.Data.EF.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Domain.Entity.Customer>
{
    public void Configure(EntityTypeBuilder<Domain.Entity.Customer> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(c => c.BirthDate)
            .IsRequired();

        builder.Property(c => c.GenderType)
            .IsRequired()
            .HasConversion<string>();

        builder.Property(c => c.CreatedAt)
            .IsRequired();

        builder.OwnsMany(c => c.Addresses, addressBuilder =>
        {
            addressBuilder.WithOwner().HasForeignKey("CustomerId");
            addressBuilder.HasKey(a => a.Id);

            addressBuilder.Property(a => a.Street).IsRequired().HasMaxLength(255);
            addressBuilder.Property(a => a.Number).IsRequired().HasMaxLength(20);
            addressBuilder.Property(a => a.Complement).HasMaxLength(255);
            addressBuilder.Property(a => a.Neighborhood).IsRequired().HasMaxLength(255);
            addressBuilder.Property(a => a.City).IsRequired().HasMaxLength(100);
            addressBuilder.Property(a => a.State).IsRequired().HasMaxLength(50);
            addressBuilder.Property(a => a.ZipCode).IsRequired().HasMaxLength(9);
        });
    }
}