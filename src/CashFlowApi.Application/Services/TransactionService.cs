
using CashFlowApi.Application.DTOs.Transactions;

namespace CashFlowApi.Application.Services;

public class TransactionService
{
    public Guid RegisterTransaction(TransactionRequest transactionRequest)
    {
        if (transactionRequest == null || string.IsNullOrEmpty(transactionRequest.Description))
        {
            throw new ArgumentException("Invalid argument");
        }

        return new Guid();
    }
}
