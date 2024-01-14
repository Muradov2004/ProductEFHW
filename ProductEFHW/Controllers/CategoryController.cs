using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductEFHW.Data;
using ProductEFHW.Models;
using ProductEFHW.Models.ViewModels;

namespace ProductEFHW.Controllers;

public class CategoryController : Controller
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public CategoryController(AppDbContext appDbContext,IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        var categories = _appDbContext.Categories.ToList();
        return View(categories);
    }
    public IActionResult Add() => View();

    [HttpPost]
    public async Task<IActionResult> Add(AddCategoryViewModel addCategory)
    {
        if (ModelState.IsValid)
        {
            var category = _mapper.Map<Category>(addCategory);
            _appDbContext.Categories.Add(category);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View(addCategory);
    }

    public IActionResult Edit(int id)
    {
        if (_appDbContext.Categories.FirstOrDefault(c => c.Id == id) is not null)
        {
            var category = new EditCategoryViewModel(_appDbContext.Categories.FirstOrDefault(c => c.Id == id)!);
            return View(category);
        }
        else
        {
            return View(null);
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(EditCategoryViewModel editCategory)
    {
        if (ModelState.IsValid)
        {
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == editCategory.Id);
            category!.Name = editCategory.Name;
            category!.ModifiedTime = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        else return View(editCategory);

    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
        var category = await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        if (category is not null)
        {
            _appDbContext.Remove(category);
            await _appDbContext.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}