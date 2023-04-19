using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rocky.Data;
using Rocky.Models;

namespace Rocky.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _db;

    public ProductController(ApplicationDbContext db)
    {
        _db = db;
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
        IEnumerable<SelectListItem> categoryDropDown =
            _db.Categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

        IEnumerable<SelectListItem> applicationTypeDropDown =
    _db.ApplicationTypes.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();


        ViewBag.CategoryDropDown = categoryDropDown;
        ViewBag.ApplicationTypeDropDown = applicationTypeDropDown;
        Product product = new Product();
        if (id == null)
        {
            //Create
            return View(product);
        }
        else
        {
            //Update
            var productFromDb = _db.Products.Find(id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Product obj)
    {
        if (!ModelState.IsValid)
        {
            return View(obj);
        }
        else
        {
            _db.Products.Update(obj);
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
