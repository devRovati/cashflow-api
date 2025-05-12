using CashFlowApi.Domain.Interfaces;
using CashFlowApi.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlowApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionRepository, TransactionRepository>();

        return services;
    }
}
