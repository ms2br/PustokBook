using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PustokBook.Areas.Admin.Helpers;
using PustokBook.Areas.Admin.ViewModels.Products;
using PustokBook.Contexts;
using PustokBook.Models;

namespace PustokBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        IWebHostEnvironment _env { get; }
        PustokDbContexts _db { get; }

        public ProductController(PustokDbContexts db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<AdminProductListItemVM> products = await _db.Products.Select(x => new AdminProductListItemVM
            {
                Id = x.Id,
                Authors = x.AuthorBook.Select(x => x.Author),
                Category = x.Category,
                CostPrice = x.CostPrice,
                Description = x.Description,
                Discount = x.Discount,
                ExTax = x.ExTax,
                IsDeleted = x.IsDeleted,
                ProductCode = x.ProductCode,
                ActiveImage = x.ActiveImage,
                Quantity = x.Quantity,
                SellPrice = x.SellPrice,
                Title = x.Title
            }).ToListAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categorys = new SelectList(_db.Categorys.Where(x => x.IsDeleted == false), "Id", "Name");

            ViewBag.Authors = new SelectList(_db.Authors.Where(x => x.IsDeleted == false), "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminProductVM data)
        {
            if (data.ExTax > data.CostPrice)
            {
                ModelState.AddModelError("CostPrice", "Ex Tax cannot be greater than the cost price");
            }

            if (data.CostPrice > data.SellPrice)
            {
                ModelState.AddModelError("SellPrice", "Cost Price cannot be greater than the sales price");
            }

            if (data.ActiveImage != null)
            {
                if (data.ActiveImage.IsValidSize(20000))
                {
                    ModelState.AddModelError("ImageFile", "Files length must be less than kb");
                }

                if (data.ActiveImage.IsCorrectType())
                {
                    ModelState.AddModelError("ImageFile", "Wrong file type");
                }
            }

            if (data.ImagesUrl != null)
            {
                foreach (IFormFile img in data.ImagesUrl)
                {
                    if (img.IsValidSize(20000))
                    {
                        ModelState.AddModelError("", "Wrong file type (" + img.FileName + ")");

                    }

                    if (img.IsCorrectType())
                    {

                        ModelState.AddModelError("", "Files length must be less than kb (" + img.FileName + ")");
                    }
                }
            }

            if (data.CategoryId < 1 || data.CategoryId == null)
            {
                ModelState.AddModelError("CategoryId", "Category doesnt exist");
                ViewBag.Authors = new SelectList(_db.Authors.Where(x => x.IsDeleted == false), "Id", "FullName");

                ViewBag.Categorys = new SelectList(_db.Categorys.Where(x => x.IsDeleted == false), "Id", "Name");
                return View(data);
            }

            if (!await _db.Categorys.AnyAsync(x => x.CategoryId == data.CategoryId))
            {
                ModelState.AddModelError("CategoryId", "Category doesnt exist");
                ViewBag.Authors = new SelectList(_db.Authors.Where(x => x.IsDeleted == false), "Id", "FullName");

                ViewBag.Categorys = new SelectList(_db.Categorys.Where(x => x.IsDeleted == false), "Id", "Name");
                return View(data);
            }

            if (await _db.Categorys.Where(c => data.AuthorIds.Contains(c.Id)).Select(c => c.Id).CountAsync() != data.AuthorIds.Count())
            {
                ModelState.AddModelError("CategoryId", "Category doesnt exist");
                ViewBag.Authors = new SelectList(_db.Authors.Where(x => x.IsDeleted == false), "Id", "FullName");

                ViewBag.Categorys = new SelectList(_db.Categorys.Where(x => x.IsDeleted == false), "Id", "Name");
                return View(data);
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Authors = new SelectList(_db.Authors.Where(x => x.IsDeleted == false), "Id", "FullName");

                ViewBag.Categorys = new SelectList(_db.Categorys.Where(x => x.IsDeleted == false), "Id", "Name");
                return View(data);
            }

            Product product = new Product
            {
                ActiveImage = await data.ActiveImage.SaveAsync(PathConstants.ProductImage),
                Title = data.Title,
                Description = data.Description,
                ProductCode = data.ProductCode,
                CategoryId = data.CategoryId,
                ExTax = data.ExTax,
                SellPrice = data.SellPrice,
                CostPrice = data.CostPrice,
                CreatedAt = data.CreatedAt,
                Discount = data.Discount,
                Quantity = data.Quantity,
                ProductImages = data.ImagesUrl.Select(x => new ProductImage
                {
                    ImageUrl = x.SaveAsync(PathConstants.ProductImage).Result
                }).ToList(),
                AuthorBook = data.AuthorIds.Select(id => new AuthorBook
                {
                    AuthorId = id
                }).ToList()
            };

            await _db.AddAsync(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
