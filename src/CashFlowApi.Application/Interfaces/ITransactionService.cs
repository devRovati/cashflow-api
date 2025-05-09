using CashFlowApi.Application.DTOs.Transactions;

namespace CashFlowApi.Application.Interfaces;

public interface ITransactionService
{
    public TransactionResponse RegisterTransaction(TransactionRequest transactionRequest);
}
