using Application.Tests.Extensions;
using CashFlowApi.Application.DTOs.Errors;
using CashFlowApi.Application.DTOs.Transactions;
using CashFlowApi.Application.Exceptions;
using CashFlowApi.Application.Interfaces;
using CashFlowApi.Application.Services;
using CashFlowApi.Domain.Entities;
using CashFlowApi.Domain.Enums;
using CashFlowApi.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.AutoMock;

namespace CashFlow.Application.Tests;

public class TransactionServiceTest
{
    private readonly AutoMocker _mocker;
    private readonly ITransactionService _transactionService;

    public TransactionServiceTest()
    {
        _mocker = new AutoMocker();
        _transactionService = _mocker.CreateInstance<TransactionService>();
    }

    [Fact]
    public async Task RegisterTransactionAsync_ValidTransaction_Should_Return_TransactionResponse()
    {
        // Arrange
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

        _mocker.GetMock<ITransactionRepository>()
            .Setup(repo => repo.CreateTransactionAsync(It.IsAny<TransactionEntity>()))
            .ReturnsAsync(It.IsAny<TransactionEntity>());

        // Act
        TransactionResponse transactionResponse = await _transactionService.RegisterTransactionAsync(transactionRequest);

        // Assert
        Assert.NotNull(transactionResponse);
        Assert.IsType<TransactionResponse>(transactionResponse);

        // Validate if the repository method was called once
        _mocker.GetMock<ITransactionRepository>()
            .Verify(repo => repo.CreateTransactionAsync(It.IsAny<TransactionEntity>()), Times.Once);
    }

    [Fact]
    public async Task RegisterTransactionAsync_Throws_InternalServerException_On_RepositoryError()
    {
        // Arrange
        var transactionRequest = new TransactionRequest
        {
            PaymentDate = DateTime.Now,
            TransactionType = TransactionType.Debit,
            Description = string.Empty,
            Amount = 1200000,
            CategoryId = 1,
            UserId = 1
        };

        _mocker.GetMock<ITransactionRepository>()
            .Setup(repo => repo.CreateTransactionAsync(It.IsAny<TransactionEntity>()))
            .ThrowsAsync(new Exception(" Access denied for user xpto"));

        // Act
        var exception = await Assert.ThrowsAsync<InternalServerException>(
            () => _transactionService.RegisterTransactionAsync(transactionRequest)
        );

        // Assert
        Assert.Equal("Unexpected internal error", exception.Error.Message);
        Assert.Equal(ErrorType.Server, exception.Error.ErrorType);
    }

    [Fact]
    public async Task RegisterTransactionAsync_Logger_Should_Log_Information_On_Success()
    {
        // Arrange
        var transactionRequest = new TransactionRequest
        {
            PaymentDate = DateTime.Now,
            TransactionType = TransactionType.Debit,
            Description = string.Empty,
            Amount = 1200000,
            CategoryId = 1,
            UserId = 1
        };

        // Act
        TransactionResponse transactionResponse = await _transactionService.RegisterTransactionAsync(transactionRequest);

        // Assert
        // Validate if the success message was logged once
        var times = Times.Once;

        _mocker.GetMock<ILogger<TransactionService>>()
            .VerifyLoggedMessage(
                LogLevel.Information,
                "RegisterTransactionAsyncMessage",
                times
            );
    }

    [Fact]
    public async Task RegisterTransactionAsync_Logger_Should_Log_Critical_On_Exception()
    {
        // Arrange
        var transactionRequest = new TransactionRequest
        {
            PaymentDate = DateTime.Now,
            TransactionType = TransactionType.Debit,
            Description = string.Empty,
            Amount = 1200000,
            CategoryId = 1,
            UserId = 1
        };

        _mocker.GetMock<ITransactionRepository>()
            .Setup(repo => repo.CreateTransactionAsync(It.IsAny<TransactionEntity>()))
            .ThrowsAsync(new Exception("Database error"));

        // Act/Assert
        await Assert.ThrowsAsync<InternalServerException>(() => _transactionService.RegisterTransactionAsync(transactionRequest));

        // Assert
        // Verify that the critcal log was written
        var times = Times.Once;

        _mocker.GetMock<ILogger<TransactionService>>()
            .VerifyLoggedMessage(
                LogLevel.Critical,
                "RegisterTransactionAsyncErrorMessage",
                times
            );
    }
}
