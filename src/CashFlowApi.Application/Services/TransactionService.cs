using CashFlowApi.Application.DTOs.Transactions;
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
        if (transactionRequest == null || string.IsNullOrEmpty(transactionRequest.Description))
        {
            _logger.LogError("RegisterTransactionMessage -> Invalid arguments, transactionRequest: {@transactionRequest}", transactionRequest);
            throw new ArgumentException("Invalid argument");
        }

        TransactionEntity transaction = new()
        {
            Amount = transactionRequest.Amount,
            CategoryId = transactionRequest.CategoryId,
            CreatedAt = DateTime.Now,
            CreatedBy = transactionRequest.UserId,
            Description = transactionRequest.Description,
            PaymentDate = transactionRequest.PaymentDate,
            PaymentMethod = transactionRequest.PaymentMethod.ToString(),
            TransactionType = transactionRequest.Type.ToString()
        };

        await _transactionRepository.CreateTransactionAsync(transaction);

        return new TransactionResponse();
    }
}
