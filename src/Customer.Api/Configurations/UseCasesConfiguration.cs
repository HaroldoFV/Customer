using Customer.Application.Interfaces;
using Customer.Application.UseCases.Customer.CreateCustomer;
using Customer.Domain.Repository;
using Customer.Infra.Data.EF;
using Customer.Infra.Data.EF.Repository;

namespace Customer.Api.Configurations;

public static class UseCasesConfiguration
{
    public static IServiceCollection AddUseCases(
        this IServiceCollection services
    )
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateCustomer).Assembly)
        );
        services.AddRepositories();
        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services
    )
    {
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}