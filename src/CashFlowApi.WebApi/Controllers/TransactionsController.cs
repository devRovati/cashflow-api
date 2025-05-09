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
    public ActionResult RegisterTransaction([FromBody] TransactionRequest request)
    {
        _transactionService.RegisterTransaction(request);
        return Created();
    }
}
