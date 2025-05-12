using CashFlowApi.Application.DTOs.Transactions;

namespace CashFlowApi.Application.Interfaces;

public interface ITransactionService
{
    public Task<TransactionResponse> RegisterTransactionAsync(TransactionRequest transactionRequest);
}
