using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PustokBook.Areas.Admin.Helpers;
using PustokBook.Areas.Admin.ViewModels.ProductVM;
using PustokBook.Contexts;
using PustokBook.Models;
using SIO = System.IO;

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

        public async Task<IActionResult> Create()
        {
            ViewBag.Categorys = new SelectList(_db.Categorys.Where(x => x.IsDeleted == false), "Id", "Name");

            ViewBag.Authors = new SelectList(_db.Authors.Where(x => x.IsDeleted == false), "Id", "FullName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAdminProductVM data)
        {
            ViewBag.Authors = new SelectList(_db.Authors.Where(x => x.IsDeleted == false), "Id", "FullName");

            ViewBag.Categorys = new SelectList(_db.Categorys.Where(x => x.IsDeleted == false), "Id", "Name");

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

                if (!data.ActiveImage.IsCorrectType())
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
                        ModelState.AddModelError("ImagesUrl", "Wrong file type (" + img.FileName + ")");

                    }

                    if (!img.IsCorrectType())
                    {

                        ModelState.AddModelError("ImagesUrl", "Files length must be less than kb (" + img.FileName + ")");
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

            if (!await _db.Categorys.AnyAsync(x => x.Id == data.CategoryId))
            {
                return View(data);
            }

            if (data.AuthorIds == null)
            {
                ModelState.AddModelError("AuthorIds", "Category doesnt exist");
                ViewBag.Authors = new SelectList(_db.Authors.Where(x => x.IsDeleted == false), "Id", "FullName");
                ViewBag.Categorys = new SelectList(_db.Categorys.Where(x => x.IsDeleted == false), "Id", "Name");
                return View(data);
            }

            if (await _db.Authors.Where(x => data.AuthorIds.Contains(x.Id)).Select(c => c.Id).CountAsync() != data.AuthorIds.Count())
            {
                return View(data);
            }

            if (!ModelState.IsValid)
            {
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
                AuthorBook = data.AuthorIds.Select(id => new AuthorProduct
                {
                    AuthorId = id
                }).ToList()
            };

            await _db.AddAsync(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int? id)
        {
            string rootParh = PathConstants.RootPath;

            if (id < 1 || id == null)
                return BadRequest();

            Product product = await _db.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            SIO.File.Delete(rootParh + "\\" + product.ActiveImage);
            product.IsDeleted = true;

            IEnumerable<ProductImage> productImages = await _db.ProductImages.Where(x => x.ProductId == id).Select(x => x).ToListAsync();

            if (productImages == null || productImages.Count() == 0)
            {
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            foreach (var item in productImages)
            {
                SIO.File.Delete(rootParh + "\\" + item.ImageUrl);
                _db.ProductImages.Remove(item);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categorys = new SelectList(_db.Categorys.Where(x => x.IsDeleted == false), "Id", "Name");
            ViewBag.Authors = new SelectList(_db.Authors.Where(x => x.IsDeleted == false), "Id", "FullName");

            if (id < 1 || id == null)
                return BadRequest();

            Product product = await _db.Products.FindAsync(id);

            UpdateProductAdminVM updateAll = new UpdateProductAdminVM
            {
                Title = product.Title,
                CategoryId = product.CategoryId,
                CostPrice = product.CostPrice,
                Description = product.Description,
                Discount = product.Discount,
                ExTax = product.ExTax,
                ProductCode = product.ProductCode,
                Quantity = product.Quantity,
                SellPrice = product.SellPrice
            };

            return View(updateAll);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateProductAdminVM updateData)
        {
            if (id < 1 || id == null)
                return BadRequest();


            if (updateData.CategoryId < 1 || updateData.CategoryId == null)
                return BadRequest();

            if (!await _db.Categorys.AnyAsync(x => x.Id == updateData.CategoryId) == null)
                return NotFound();

            if (await _db.Authors.Where(x => updateData.AuthorIds.Contains(x.Id)).Select(c => c.Id).CountAsync() != updateData.AuthorIds.Count())
                return BadRequest();


            if (updateData.ExTax > updateData.CostPrice)
            {
                ModelState.AddModelError("CostPrice", "Ex Tax cannot be greater than the cost price");
            }

            if (updateData.CostPrice > updateData.SellPrice)
            {
                ModelState.AddModelError("SellPrice", "Cost Price cannot be greater than the sales price");
            }

            if (updateData.ActiveImage != null)
            {
                if (updateData.ActiveImage.IsValidSize(20000))
                {
                    ModelState.AddModelError("ImageFile", "Files length must be less than kb");
                }

                if (updateData.ActiveImage.IsCorrectType())
                {
                    ModelState.AddModelError("ImageFile", "Wrong file type");
                }
            }

            if (updateData.ImagesUrl != null)
            {
                foreach (IFormFile img in updateData.ImagesUrl)
                {
                    if (img.IsValidSize(20000))
                    {
                        ModelState.AddModelError("ImagesUrl", "Wrong file type (" + img.FileName + ")");

                    }

                    if (img.IsCorrectType())
                    {

                        ModelState.AddModelError("ImagesUrl", "Files length must be less than kb (" + img.FileName + ")");
                    }
                }
            }


            if (!ModelState.IsValid)
            {
                ViewBag.Categorys = new SelectList(_db.Categorys.Where(x => x.IsDeleted == false), "Id", "Name");
                ViewBag.Authors = new SelectList(_db.Authors.Where(x => x.IsDeleted == false), "Id", "FullName");
                return View(updateData);
            }

            Product product = await _db.Products.FindAsync(id);

            ICollection<ProductImage> productImages = await _db.ProductImages.Where(x => x.ProductId == id).ToListAsync();

            if (product == null || productImages.Count == 0 || productImages == null)
                return NotFound();

            product.UpdatedAt = updateData.UpdatedAt;
            product.SellPrice = updateData.SellPrice;
            product.CostPrice = updateData.CostPrice;
            product.ProductCode = updateData.ProductCode;
            product.Title = updateData.Title;
            product.Description = updateData.Description;
            product.Quantity = updateData.Quantity;
            product.ExTax = updateData.ExTax;
            product.Discount = updateData.Discount;
            product.CategoryId = updateData.CategoryId;
            product.AuthorBook = updateData.AuthorIds.Select(id => new AuthorProduct
            {
                AuthorId = id
            }).ToList();
            product.ActiveImage = updateData.ActiveImage.SaveAsync(PathConstants.ProductImage).Result;
            product.ProductImages = updateData.ImagesUrl.Select(x => new ProductImage
            {
                ImageUrl = x.SaveAsync(PathConstants.ProductImage).Result
            }).ToList();
            return RedirectToAction(nameof(Index));
        }
    }
}
