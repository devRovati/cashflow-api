using CashFlowApi.Application.DTOs.Transactions;
using CashFlowApi.Application.Validators;
using CashFlowApi.Domain.Enums;
using FluentValidation.Results;
using Moq.AutoMock;

namespace CashFlow.Application.Tests;

public class TransactionRequestValidatorTest
{
    private readonly AutoMocker _mocker;
    private readonly TransactionRequestValidator _transactionRequestValidator;

    public TransactionRequestValidatorTest()
    {
        _mocker = new AutoMocker();
        _transactionRequestValidator = _mocker.CreateInstance<TransactionRequestValidator>();
    }

    [Fact]
    public async Task TransactionRequestValidator_Validation_Should_Succeed_With_Valid_Request()
    {
        // Arrange
        TransactionRequest transactionRequest = new()
        {
            Amount = 1200000,
            CategoryId = 1,
            Description = "Venda de 1 veiculo 0km",
            PaymentDate = DateTime.Now,
            PaymentMethod = PaymentMethodType.Invoice,
            TransactionType = TransactionType.Debit,
            UserId = 1
        };

        // Act
        ValidationResult validationResult = await _transactionRequestValidator.ValidateAsync(transactionRequest);

        // Arrange
        Assert.NotNull(validationResult);
        Assert.True(validationResult.IsValid);
    }

    [Fact]
    public async Task TransactionRequestValidator_Validation_Validation_Should_Fail_When_Description_Is_Empty()
    {
        // Arrange
        TransactionRequest transactionRequest = new()
        {
            Amount = 1200000,
            CategoryId = 1,
            Description = "",
            PaymentDate = DateTime.Now,
            PaymentMethod = PaymentMethodType.Invoice,
            TransactionType = TransactionType.Debit,
            UserId = 1
        };

        // Act
        ValidationResult validationResult = await _transactionRequestValidator.ValidateAsync(transactionRequest);

        // Arrange
        Assert.NotNull(validationResult);
        Assert.False(validationResult.IsValid);
    }

    [Fact]
    public async Task TransactionRequestValidator_Validation_Should_Fail_When_Amount_Is_Less_Or_Equal_Than_Zero()
    {
        // Arrange
        TransactionRequest transactionRequest = new()
        {
            Amount = 0,
            CategoryId = 1,
            Description = "Some description",
            PaymentDate = DateTime.Now,
            PaymentMethod = PaymentMethodType.Invoice,
            TransactionType = TransactionType.Debit,
            UserId = 1
        };

        // Act
        ValidationResult validationResult = await _transactionRequestValidator.ValidateAsync(transactionRequest);

        // Arrange
        Assert.NotNull(validationResult);
        Assert.False(validationResult.IsValid);
    }

    [Fact]
    public async Task TransactionRequestValidator_Validation_Should_Fail_When_PaymentDate_Is_Null()
    {
        // Arrange
        TransactionRequest transactionRequest = new()
        {
            Amount = 110,
            CategoryId = 1,
            Description = "Some description",
            PaymentMethod = PaymentMethodType.Invoice,
            TransactionType = TransactionType.Debit,
            UserId = 1
        };

        // Act
        ValidationResult validationResult = await _transactionRequestValidator.ValidateAsync(transactionRequest);

        // Arrange
        Assert.NotNull(validationResult);
        Assert.False(validationResult.IsValid);
    }

    [Fact]
    public async Task TransactionRequestValidator_Validation_Should_Fail_When_PaymentDate_Is_Invalid()
    {
        // Arrange
        TransactionRequest transactionRequest = new()
        {
            Amount = 110,
            CategoryId = 1,
            Description = "Some description",
            PaymentDate = DateTime.MinValue,
            PaymentMethod = PaymentMethodType.Invoice,
            TransactionType = TransactionType.Debit,
            UserId = 1
        };

        // Act
        ValidationResult validationResult = await _transactionRequestValidator.ValidateAsync(transactionRequest);

        // Arrange
        Assert.NotNull(validationResult);
        Assert.False(validationResult.IsValid);
    }

    [Fact]
    public async Task TransactionRequestValidator_Validation_Should_Fail_When_PaymentMethod_Is_Invalid()
    {
        // Arrange
        TransactionRequest transactionRequest = new()
        {
            Amount = 0,
            CategoryId = 1,
            Description = "Some description",
            PaymentDate = DateTime.Now,
            PaymentMethod = 0,
            TransactionType = TransactionType.Debit,
            UserId = 1
        };

        // Act
        ValidationResult validationResult = await _transactionRequestValidator.ValidateAsync(transactionRequest);

        // Arrange
        Assert.NotNull(validationResult);
        Assert.False(validationResult.IsValid);
    }

    [Fact]
    public async Task TransactionRequestValidator_Validation_Should_Fail_When_TransactionType_Is_Invali()
    {
        // Arrange
        TransactionRequest transactionRequest = new()
        {
            Amount = 0,
            CategoryId = 1,
            Description = "Some description",
            PaymentDate = DateTime.Now,
            PaymentMethod = PaymentMethodType.Pix,
            TransactionType = 0,
            UserId = 1
        };

        // Act
        ValidationResult validationResult = await _transactionRequestValidator.ValidateAsync(transactionRequest);

        // Arrange
        Assert.NotNull(validationResult);
        Assert.False(validationResult.IsValid);
    }
}
