using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CashFlowApi.Domain.Entities;

[Table("categories")]
public class CategoryEntity : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("category_id")]
    public int CategoryId { get; set; }

    [Required]
    [MaxLength(30)]
    [Column("category_name")]
    public string CategoryName { get; set; }

    [Required]
    [MaxLength(80)]
    [Column("description")]
    public string Description { get; set; }
}
