using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace CashFlowApi.Application.DTOs.Errors;

public class Error
{
    [SwaggerSchema("Error message")]
    [JsonProperty("message")]
    public string Message { get; set; }

    [SwaggerSchema("Detailed error message (if exists)")]
    [JsonProperty("detailedMessage")]
    public string? DetailedMessage { get; set; }

    public Error()
    {
        Message = string.Empty;
        DetailedMessage = string.Empty;
    }

    public Error(string message, string detailedMessage)
    {
        Message = message;
        DetailedMessage = detailedMessage;
    }
}
