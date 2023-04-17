using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;

namespace Rocky.Controllers;

public class ApplicationTypeController : Controller
{
    private readonly ApplicationDbContext _db;

    public ApplicationTypeController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var objList = _db.ApplicationTypes.ToList();
        return View(objList);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(ApplicationType applicationType)
    {
        if (!ModelState.IsValid)
        {
            return View(applicationType);
        }
        else
        {
            _db.Add(applicationType);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == 0 || id == null) return NotFound();
        var obj = _db.ApplicationTypes.Find(id);
        if (obj == null) return NotFound();
        return View(obj);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(ApplicationType obj)
    {
        if (!ModelState.IsValid)
        {
            return View(obj);
        }
        else
        {
            _db.ApplicationTypes.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }


    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == 0 || id == null) return NotFound();
        var applicationType = _db.ApplicationTypes.Find(id);
        if (applicationType == null) return NotFound();
        return View(applicationType);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(ApplicationType obj)
    {
        if (!ModelState.IsValid)
        {
            return View(obj);
        }
        else
        {
            _db.ApplicationTypes.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
