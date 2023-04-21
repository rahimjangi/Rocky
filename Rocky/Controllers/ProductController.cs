using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rocky.Data;
using Rocky.Models;
using Rocky.ViewModels;

namespace Rocky.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var objList = _db.Products.Include(p => p.Category).Include(p => p.ApplicationType).ToList();
        return View(objList);
    }

    [HttpGet]
    public IActionResult Upsert(int? id)
    {
        ProductVM productVM = new ProductVM();
        IEnumerable<SelectListItem> categoryDropDown =
            _db.Categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

        IEnumerable<SelectListItem> applicationTypeDropDown =
    _db.ApplicationTypes.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

        productVM.CategorySelectList = categoryDropDown;
        productVM.ApplicationTypeSelectList = applicationTypeDropDown;


        if (id == null)
        {
            //Create
            return View(productVM);
        }
        else
        {
            //Update
            productVM.Product = _db.Products.Find(id);
            if (productVM.Product == null)
            {
                return NotFound();
            }
            return View(productVM);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(ProductVM obj)
    {
        if (!ModelState.IsValid)
        {
            return View(obj);
        }
        else
        {
            _db.Products.Update(obj.Product!);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }




    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == 0 || id == null) return NotFound();
        var obj = _db.Products.Find(id);
        if (obj == null) return NotFound();
        return View(obj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Product obj)
    {

        _db.Products.Remove(obj);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

}
