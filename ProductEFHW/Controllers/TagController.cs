using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductEFHW.Data;
using ProductEFHW.Models;
using ProductEFHW.Models.ViewModels;

namespace ProductEFHW.Controllers;

public class TagController : Controller
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public TagController(AppDbContext appDbContext,IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        var tags = _appDbContext.Tags.ToList();
        return View(tags);
    }

    public IActionResult Add() => View();

    [HttpPost]
    public async Task<IActionResult> Add(AddTagViewModel addTag)
    {
        if (ModelState.IsValid)
        {
            var tag = _mapper.Map<Tag>(addTag);
            _appDbContext.Tags.Add(tag);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View(addTag);
    }

    public IActionResult Edit(int id)
    {
        if (_appDbContext.Tags.FirstOrDefault(t => t.Id == id) is not null)
        {
            var tag = new EditTagViewModel(_appDbContext.Tags.FirstOrDefault(t => t.Id == id)!);
            return View(tag);
        }
        else
        {
            return View(null);
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(EditTagViewModel editTag)
    {
        if (ModelState.IsValid)
        {
            var tag = _appDbContext.Tags.FirstOrDefault(t => t.Id == editTag.Id);
            tag!.Name = editTag.Name;
            tag!.ModifiedTime = DateTime.Now;
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        else return View(editTag);
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
        var tag = await _appDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        if (tag is not null)
        {
            _appDbContext.Remove(tag);
            await _appDbContext.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}