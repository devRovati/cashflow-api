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


    public TransactionResponse RegisterTransaction(TransactionRequest transactionRequest)
    {
        if (transactionRequest == null || string.IsNullOrEmpty(transactionRequest.Description))
        {
            _logger.LogError("RegisterTransactionMessage -> Invalid arguments, transactionRequest: {@transactionRequest}", transactionRequest);
            throw new ArgumentException("Invalid argument");
        }

        var transaction = new TransactionEntity();
        _transactionRepository.CreateTransactionAsync(transaction);

        return new TransactionResponse();
    }
}
