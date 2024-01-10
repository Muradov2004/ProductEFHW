using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductEFHW.Data;
using ProductEFHW.Models;
using ProductEFHW.Models.ViewModels;

namespace ProductEFHW.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext _appDbContext;

    public ProductController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public IActionResult Index()
    {
        var products = _appDbContext.Products.Include(p => p.Category).ToList();
        return View(products);
    }

    public IActionResult Add()
    {
        var categories = _appDbContext.Categories.ToList();
        ViewData["Categories"] = categories;
        var tags = _appDbContext.Tags.ToList();
        ViewData["Tags"] = tags;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddProductViewModel product)
    {
        if (ModelState.IsValid)
        {
            var productTags = _appDbContext.Tags.Where(t => product.TagIds!.Contains(t.Id)).ToList();

            var newProduct = new Product
            {
                CategoryId = product.CategoryId,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Title = product.Title,
                Tags = productTags
            };
            _appDbContext.Products.Add(newProduct);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        var categories = _appDbContext.Categories.ToList();
        ViewData["Categories"] = categories;
        var tags = _appDbContext.Tags.ToList();
        ViewData["Tags"] = tags;
        return View(product);
    }


    public IActionResult Edit(int id)
    {

        var product = _appDbContext.Products.Include(p => p.Tags).FirstOrDefault(p => p.Id == id);

        if (product is not null)
        {
            var categories = _appDbContext.Categories.ToList();
            ViewData["Categories"] = categories;
            var tags = _appDbContext.Tags.ToList();
            ViewData["Tags"] = tags;

            var editProductViewModel = new EditProductViewModel
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Title = product.Title,
                TagIds = product.Tags.Select(t => t.Id).ToList()
            };

            return View(editProductViewModel);
        }
        else
        {
            return View(null);
        }
    }

    [HttpPost]
    public async Task<IActionResult> EditAsync(EditProductViewModel editProduct)
    {
        if (ModelState.IsValid)
        {
            var product = await _appDbContext.Products
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == editProduct.Id);

            if (product == null)
            {
                return NotFound();
            }

            product.CategoryId = editProduct.CategoryId;
            product.Price = editProduct.Price;
            product.Description = editProduct.Description;
            product.ImageUrl = editProduct.ImageUrl;
            product.Title = editProduct.Title;


            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        else
        {
            return View(editProduct);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _appDbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product is not null)
        {
            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}
