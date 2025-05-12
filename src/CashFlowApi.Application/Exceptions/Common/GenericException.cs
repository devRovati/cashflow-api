using CashFlowApi.Application.DTOs.Errors;

namespace CashFlowApi.Application.Exceptions.Common;

public class GenericException : Exception
{
    public ErrorResponse Error { get; set; }

    public GenericException(ErrorResponse error)
    {
        Error = error;
    }
}
