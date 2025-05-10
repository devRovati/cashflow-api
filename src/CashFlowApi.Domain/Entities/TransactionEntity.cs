using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CashFlowApi.Domain.Enums;

namespace CashFlowApi.Domain.Entities;

[Table("transactions")]
public class TransactionEntity : BaseEntity 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("transaction_id")]
    public int TransactionId { get; set; }

    [Required]
    [Column("payment_date")]
    public DateTime PaymentDate { get; set; }

    [Required]
    [EnumDataType(typeof(TransactionType))]
    [MaxLength(20)]
    [Column("transaction_type")]
    public string TransactionType { get; set; }

    [Required]
    [MaxLength(255)]
    [Column("description")]
    public string Description { get; set; }

    [Required]
    [Column("amount", TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required]
    [MaxLength(20)]
    [EnumDataType(typeof(PaymentMethodType))]
    [Column("payment_method")]
    public string PaymentMethod { get; set; }

    [Required]
    [Column("category_id")]
    public int CategoryId { get; set; }
}
