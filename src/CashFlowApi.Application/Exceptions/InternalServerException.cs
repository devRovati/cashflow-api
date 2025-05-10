using CashFlowApi.Application.DTOs.Errors;
using CashFlowApi.Application.Exceptions.Common;

namespace CashFlowApi.Application.Exceptions;

public class InternalServerException : GenericException
{
    public InternalServerException(ErrorResponse error) : base(error)
    { }
}
