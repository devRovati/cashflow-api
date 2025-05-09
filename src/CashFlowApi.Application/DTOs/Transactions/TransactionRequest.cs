namespace CashFlowApi.Application.DTOs.Transactions;

public class TransactionRequest
{
    public DateTime Date { get; set; }

    public string Type { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }
    
    public Guid CategoryId { get; set; }
    
    public Guid UserId { get; set; }
    
    public Guid? CustomerOrSupplierId { get; set; }
    
    public string PaymentMethod { get; set; }

    public TransactionRequest() { }
}
