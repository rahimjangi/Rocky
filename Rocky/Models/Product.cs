
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rocky.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    [Range(0.99, double.MaxValue)]
    public decimal Price { get; set; }
    public string Image { get; set; } = string.Empty;
    [Display(Name = "Category Type")]
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public virtual Category? Category { get; set; }
    [Display(Name = "Application Type")]
    public int ApplicationTypeId { get; set; }
    [ForeignKey("ApplicationTypeId")]
    public virtual ApplicationType? ApplicationType { get; set; }

}
