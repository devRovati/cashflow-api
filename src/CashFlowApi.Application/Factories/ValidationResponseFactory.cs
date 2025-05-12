using CashFlowApi.Application.DTOs.Errors;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowApi.Application.Factories;

public static class ValidationResponseFactory
{
    public static IActionResult ToBadRequest(this ValidationResult validationResult)
    {
        List<Error> errors = validationResult.Errors
            .Select(x => new Error { Message = x.ErrorMessage })
            .ToList();

        ErrorResponse errorResponse = new()
        {
            ErrorType = ErrorType.BadRequest,
            Errors = errors,
            Message = "One or more errors occurred while validating the request"
        };

        return new BadRequestObjectResult(errorResponse);
    }
}
