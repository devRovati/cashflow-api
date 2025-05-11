using CashFlowApi.Application.DTOs.Transactions;
using CashFlowApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;
using CashFlowApi.Application.Factories;
using Swashbuckle.AspNetCore.Annotations;
using CashFlowApi.Application.DTOs.Errors;
using Swashbuckle.AspNetCore.Filters;
using CashFlowApi.WebApi.Examples;

namespace CashFlowApi.WebApi.Controllers;

[ApiController]
[Route("api/transactions")]
[Produces("application/json")]
[Consumes("application/json")]
public class TransactionsController : ControllerBase
{
    private readonly IValidator<TransactionRequest> _validator;
    private readonly ITransactionService _transactionService;

    public TransactionsController(IValidator<TransactionRequest> validator, ITransactionService transactionService)
    {
        _validator = validator;
        _transactionService = transactionService;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Register a new transaction.")]
    [SwaggerResponse(StatusCodes.Status201Created, "The transaction was created successfully.", typeof(TransactionResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Some error was found in the request.", typeof(ErrorResponse))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestErrorExample))]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Something went wrong on the server side.", typeof(ErrorResponse))]
    [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(InternalServerErrorExample))]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterTransaction([FromBody] TransactionRequest request)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return ValidationResponseFactory.ToBadRequest(validationResult);

        TransactionResponse transactionResponse = await _transactionService.RegisterTransactionAsync(request);
        return Created("", transactionResponse);
    }
}
