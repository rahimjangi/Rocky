using System.ComponentModel.DataAnnotations;

namespace Rocky.Models;

public class Category
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "You need to enter the name")]
    public string Name { get; set; } = string.Empty;
    [Display(Name = "Display Order")]
    [Required]
    [Range(1, 100, ErrorMessage = "Order should be in range (1,100)")]
    public int DisplayOrder { get; set; } = 1;

}
