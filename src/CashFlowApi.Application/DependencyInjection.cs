using CashFlowApi.Application.Interfaces;
using CashFlowApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using CashFlowApi.Application.DTOs.Transactions;
using CashFlowApi.Application.Validators;

namespace CashFlowApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IValidator<TransactionRequest>, TransactionRequestValidator>();

        return services;
    }
}
