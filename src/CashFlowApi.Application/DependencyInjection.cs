using CashFlowApi.Application.Interfaces;
using CashFlowApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlowApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionService, TransactionService>();

        return services;
    }
}
