using CashFlowApi.Application.DTOs.Errors;
using CashFlowApi.Application.DTOs.Transactions;
using CashFlowApi.Application.Exceptions;
using CashFlowApi.Application.Interfaces;
using CashFlowApi.Domain.Entities;
using CashFlowApi.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace CashFlowApi.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ILogger<TransactionService> _logger;
    private readonly ITransactionRepository _transactionRepository;

    public TransactionService(ILogger<TransactionService> logger, ITransactionRepository transactionRepository)
    {
        _logger = logger;
        _transactionRepository = transactionRepository;
    }

    public async Task<TransactionResponse> RegisterTransactionAsync(TransactionRequest transactionRequest)
    {
        _logger.LogDebug("RegisterTransactionAsyncParams -> {@transactionRequest}", transactionRequest);

        TransactionEntity transaction = MapToTransactionEntity(transactionRequest);

        try
        {
            await _transactionRepository.CreateTransactionAsync(transaction);
            _logger.LogInformation("RegisterTransactionAsyncMessage -> Transaction created successfully: {transactionId}", transaction.TransactionId);
        }
        catch (Exception ex)
        {
            _logger.LogCritical("RegisterTransactionAsyncErrorMessage -> Error creating transaction. ErrorMessage: {errorMessage}", ex.Message);

            throw new InternalServerException(new()
            {
                Message = "Unexpected internal error",
                ErrorType = ErrorType.Server,
                Errors = [ new() { Message = "An error occurred while trying to save the transaction" } ]
            });
        }

        return new TransactionResponse
        {
            CreatedAt = transaction.CreatedAt,
            TransactionId = transaction.TransactionId
        };
    }

    #region private-methods

    private TransactionEntity MapToTransactionEntity(TransactionRequest request)
    {
        return new TransactionEntity
        {
            Amount = request.Amount,
            CategoryId = request.CategoryId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = request.UserId,
            Description = request.Description,
            PaymentDate = request.PaymentDate,
            PaymentMethod = request.PaymentMethod.ToString(),
            TransactionType = request.TransactionType.ToString()
        };
    }

    #endregion
}
