using CashFlowApi.Application.DTOs.Errors;
using Swashbuckle.AspNetCore.Filters;

namespace CashFlowApi.WebApi.Examples;

public class InternalServerErrorExample : IExamplesProvider<ErrorResponse>
{
    public ErrorResponse GetExamples()
    {
        return new ErrorResponse
        {
            Message = "Unexpected internal error.",
            ErrorType = ErrorType.Server,
            Errors = [new Error { Message = "An unexpected error occurred." }]
        };
    }
}
