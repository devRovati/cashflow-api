using CashFlowApi.Domain.Entities;
using CashFlowApi.Domain.Interfaces;
using CashFlowApi.Infrastructure.Persistence;

namespace CashFlowApi.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TransactionEntity> CreateTransactionAsync(TransactionEntity transaction)
    {
        await _context.AddAsync<TransactionEntity>(transaction);
        await _context.SaveChangesAsync();

        return transaction;
    }
}
