﻿using Microsoft.AspNetCore.Mvc;
using Rocky.Data;
using Rocky.Models;

namespace Rocky.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;

    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    [HttpGet]
    public IActionResult Index()
    {
        var objList = _db.Categories.ToList();
        return View(objList);
    }
    [HttpGet]
    public IActionResult Upsert(int? id)
    {
        Category Category = new Category();
        if (id == null)
        {
            return View(Category);
        }
        else
        {
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }
        else
        {
            if (category.Id == 0)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
    //[HttpGet]
    //public IActionResult Edit(int? id)
    //{
    //    if (id == 0 || id == null) return NotFound();
    //    var category = _db.Categories.Find(id);
    //    if (category == null) return NotFound();
    //    return View(category);
    //}

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult Edit(Category category)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return View(category);
    //    }
    //    else
    //    {
    //        _db.Categories.Update(category);
    //        _db.SaveChanges();
    //        return RedirectToAction("Index");
    //    }
    //}


    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == 0 || id == null) return NotFound();
        var category = _db.Categories.Find(id);
        if (category == null) return NotFound();
        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }
        else
        {
            _db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}
