using CashFlowApi.Domain.Enums;

namespace CashFlowApi.Application.DTOs.Transactions;

public class TransactionRequest
{
    public DateTime PaymentDate { get; set; }

    public TransactionType Type { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }
    
    public int CategoryId { get; set; }
    
    public int UserId { get; set; }
    
    public int? CustomerOrSupplierId { get; set; }
    
    public PaymentMethodType PaymentMethod { get; set; }

    public TransactionRequest() { }
}
