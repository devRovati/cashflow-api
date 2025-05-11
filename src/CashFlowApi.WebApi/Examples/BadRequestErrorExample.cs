using CashFlowApi.Application.DTOs.Errors;
using Swashbuckle.AspNetCore.Filters;

namespace CashFlowApi.WebApi.Examples;

public class BadRequestErrorExample : IExamplesProvider<ErrorResponse>
{
    public ErrorResponse GetExamples()
    {
        return new ErrorResponse
        {
            Message = "Invalid request data.",
            ErrorType = ErrorType.BadRequest,
            Errors = [new Error { Message = "Description is required." }]
        };
    }
}
