using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CashFlowApi.Application.DTOs.Errors;

[JsonConverter(typeof(StringEnumConverter))]
public enum ErrorType
{
    BadRequest = 1,
    Server = 2
}
