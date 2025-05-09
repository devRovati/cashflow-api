
namespace CashFlowApi.Domain.Entities;

public class BaseEntity
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }
}
