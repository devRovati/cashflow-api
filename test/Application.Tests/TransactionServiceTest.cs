using CashFlowApi.Application.DTOs.Transactions;
using CashFlowApi.Application.Services;

namespace Application.Tests;

public class TransactionServiceTest
{
    private readonly TransactionService _transactionService;

    public TransactionServiceTest()
    {
        _transactionService = new TransactionService();
    }

    [Fact]
    public void RegisterTransaction_WithValidData_Should_Be_Successful()
    {
        var transactionRequest = new TransactionRequest
        {
            Date = DateTime.Now,
            Type = "Credit",
            Description = "Venda de Produto",
            Amount = 100.00m,
            CategoryId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        Guid transactionId = _transactionService.RegisterTransaction(transactionRequest);

        Assert.Equal(transactionId, new Guid());
    }
}
