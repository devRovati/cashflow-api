using CashFlowApi.Application.DTOs.Transactions;
using CashFlowApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.Results;

namespace CashFlowApi.WebApi.Controllers;

[Route("api/transactions")]
[ApiController]
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
    [AllowAnonymous]
    public async Task<IActionResult> RegisterTransaction([FromBody] TransactionRequest request)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(new
            {
                errors = validationResult.Errors.Select(x => new
                    {
                        errorMessage = x.ErrorMessage,
                        propertyName = x.PropertyName
                    }
                )
            });
        }

        TransactionResponse transactionResponse = await _transactionService.RegisterTransactionAsync(request);
        return Created("", transactionResponse);
    }
}
