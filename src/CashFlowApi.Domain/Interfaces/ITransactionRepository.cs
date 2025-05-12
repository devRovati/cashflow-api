using CashFlowApi.Domain.Entities;

namespace CashFlowApi.Domain.Interfaces;

public interface ITransactionRepository
{
    public Task<TransactionEntity> CreateTransactionAsync(TransactionEntity transaction);
}
