using CashFlowApi.Application.DTOs.Errors;
using CashFlowApi.Application.Factories;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Application.Tests;

public class ValidationResponseFactoryTest
{

    public ValidationResponseFactoryTest()
    {}

    [Fact]
    public void ToBadRequest_Should_Return_Correct_Response()
    {
        // Arrange
        var validationResult = new ValidationResult(new List<ValidationFailure>
        {
            new("Amount", "Amount must be greater than zero"),
            new("UserId", "UserId is required")
        });

        // Act
        IActionResult result = validationResult.ToBadRequest();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var errorResponse = Assert.IsType<ErrorResponse>(badRequestResult.Value);

        Assert.Equal(ErrorType.BadRequest, errorResponse.ErrorType);
        Assert.Equal("One or more errors occurred while validating the request", errorResponse.Message);
        Assert.Equal(2, errorResponse.Errors.Count);
        Assert.Contains(errorResponse.Errors, e => e.Message == "Amount must be greater than zero");
        Assert.Contains(errorResponse.Errors, e => e.Message == "UserId is required");
    }
}
