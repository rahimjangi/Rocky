using Microsoft.AspNetCore.Mvc.Rendering;
using Rocky.Models;

namespace Rocky.ViewModels;

public class ProductVM
{
    public Product? Product { get; set; } = new Product();
    public IEnumerable<SelectListItem>? CategorySelectList { get; set; }
    public IEnumerable<SelectListItem>? ApplicationTypeSelectList { get; set; }
}
