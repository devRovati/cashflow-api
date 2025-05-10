using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowApi.Application.Factories;

public static class ValidationResponseFactory
{
    public static IActionResult ToBadRequest(this ValidationResult validationResult)
    {
        var errors = validationResult.Errors.Select(x => new
        {
            errorMessage = x.ErrorMessage,
            propertyName = x.PropertyName
        })
        .ToList();

        return new BadRequestObjectResult(errors);
    }
}
