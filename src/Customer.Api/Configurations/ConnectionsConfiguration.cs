using Customer.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace Customer.Api.Configurations;

public static class ConnectionsConfiguration
{
    public static IServiceCollection AddAppConnections(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbConnection(configuration);

        services.AddHealthChecks()
            .AddSqlServer(
                configuration.GetConnectionString("CustomerDb")
                ?? throw new Exception("SQL Server configuration section not found")
            );

        return services;
    }

    private static IServiceCollection AddDbConnection(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration
            .GetConnectionString("CustomerDb");

        services.AddDbContext<CustomerDbContext>(
            options => options.UseSqlServer(
                    connectionString
                    ?? throw new Exception("SQL Server configuration section not found")
                )
                .LogTo(Console.WriteLine, LogLevel.Information)
        );

        return services;
    }
}