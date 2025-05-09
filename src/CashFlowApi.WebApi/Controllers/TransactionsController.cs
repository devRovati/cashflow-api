using CashFlowApi.Application.DTOs.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashFlowApi.WebApi.Controllers;

[Route("api/transactions")]
[ApiController]
public class TransactionsController : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public ActionResult RegisterTransaction(TransactionRequest request)
    {
        return Created();
    }
}
