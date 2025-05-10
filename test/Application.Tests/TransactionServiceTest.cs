using CashFlowApi.Application.DTOs.Transactions;
using CashFlowApi.Application.Services;
using CashFlowApi.Domain.Enums;
using Moq.AutoMock;

namespace Application.Tests;

public class TransactionServiceTest
{
    private readonly TransactionService _transactionService;

    public TransactionServiceTest()
    {
        var mocker = new AutoMocker();
        _transactionService = mocker.CreateInstance<TransactionService>();
    }

    [Fact]
    public async Task RegisterTransaction_Debit_WithValidData_Should_Be_Successful()
    {
        var transactionRequest = new TransactionRequest
        {
            Amount = 1200000,
            CategoryId = 1,
            Description = "Venda de 1 veiculo 0km",
            PaymentDate = DateTime.Now,
            PaymentMethod = PaymentMethodType.Invoice,
            TransactionType = TransactionType.Debit,
            UserId = 1
        };

        TransactionResponse transactionResponse = await _transactionService.RegisterTransactionAsync(transactionRequest);

        Assert.NotNull(transactionResponse);
    }

    [Fact]
    public async Task RegisterTransaction_Debit_WithIValidData_Should_Throws_Exception()
    {
        var transactionRequest = new TransactionRequest
        {
            PaymentDate = DateTime.Now,
            TransactionType = TransactionType.Debit,
            Description = string.Empty,
            Amount = 1200000,
            CategoryId = 1,
            UserId = 1
        };

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            async () => await _transactionService.RegisterTransactionAsync(transactionRequest)
        );

        Assert.IsType<ArgumentException>(exception);
    }
}
