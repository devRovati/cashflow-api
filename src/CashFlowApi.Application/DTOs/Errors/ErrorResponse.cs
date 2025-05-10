using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace CashFlowApi.Application.DTOs.Errors;

public class ErrorResponse
{
    [SwaggerSchema("Error message")]
    [JsonProperty("message")]
    public string Message { get; set; }

    [SwaggerSchema("A list of errors")]
    [JsonProperty("errors")]
    public List<Error> Errors { get; set; }

    [SwaggerSchema("Error type")]
    [JsonProperty("errorType")]
    public ErrorType ErrorType { get; set; }

    public ErrorResponse()
    {
        Message = string.Empty;
        Errors = [];
    }

    public ErrorResponse(string message, ErrorType errorTypeEnum, List<Error> errors)
    {
        Message = message;
        ErrorType = errorTypeEnum;
        Errors = errors;
    }

    public bool HasError()
    {
        return !string.IsNullOrEmpty(Message) && ErrorType != 0;
    }

    public override string ToString()
        => JsonConvert.SerializeObject(this);
}
