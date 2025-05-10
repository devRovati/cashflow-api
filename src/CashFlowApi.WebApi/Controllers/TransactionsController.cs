using CashFlowApi.Application.DTOs.Transactions;
using CashFlowApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowApi.WebApi.Controllers;

[Route("api/transactions")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterTransaction([FromBody] TransactionRequest request)
    {
        TransactionResponse transactionResponse = await _transactionService.RegisterTransactionAsync(request);
        return Created("", transactionResponse);
    }
}
