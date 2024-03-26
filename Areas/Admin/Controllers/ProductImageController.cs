using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PustokBook.Areas.Admin.Helpers;
using PustokBook.Areas.Admin.ViewModels.CommonVM;
using PustokBook.Areas.Admin.ViewModels.ProductImageVM;
using PustokBook.Contexts;
using PustokBook.Models;
using SIO = System.IO;
namespace PustokBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin, Admin, Moderator")]
    public class ProductImageController : Controller
    {

        public IWebHostEnvironment _env { get; }
        public PustokDbContexts _db { get; }

        public ProductImageController(PustokDbContexts db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            int page = 1;
            int count = 3;

            IQueryable<AdminProductImageListItemVM> item = _db.ProductImages.Select(x => new AdminProductImageListItemVM
            {
                Id = x.Id,
                ImageUrls = x.ImageUrl,
                ProductName = x.Product.Title
            }).Skip((page - 1) * count)
            .Take(count);

            int totalCount = await _db.ProductImages.CountAsync();

            PaginatonVM<IEnumerable<AdminProductImageListItemVM>> data = new(totalCount, page, (int)Math.Ceiling((decimal)totalCount / count), item);
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            List<AdminProductImageListItemVM> listItem =
               await _db.Products.Where(x => !x.IsDeleted).Select(data => new AdminProductImageListItemVM
               {
                   Id = data.Id,
                   ProductName = data.Title
               }).ToListAsync();
            ViewBag.Products = new SelectList(listItem, "Id", "ProductName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AdminCreateProductImageVM item)
        {
            List<AdminProductImageListItemVM> listItem =
               await _db.Products.Where(x => !x.IsDeleted).Select(data => new AdminProductImageListItemVM
               {
                   Id = data.Id,
                   ProductName = data.Title
               }).ToListAsync();
            ViewBag.Products = new SelectList(listItem, "Id", "ProductName");

            if (item.ProductId < 1 || item.ProductId == null) return BadRequest();

            if (!await _db.Products.AnyAsync(x => x.Id == item.ProductId))
                return NotFound();

            if (item.ImageUrl != null)
            {
                if (!item.ImageUrl.IsCorrectType())
                {
                    ModelState.AddModelError("ImageUrl", "Wrong file type");
                }

                if (item.ImageUrl.IsValidSize(20000))
                {

                    ModelState.AddModelError("ImageUrl", "Files length must be less than kb");
                }
            }

            if (!ModelState.IsValid) return View(item);

            await _db.ProductImages.AddAsync(new ProductImage
            {
                ImageUrl = item.ImageUrl.SaveAsync(PathConstants.ProductImage).Result,
                Product = await _db.Products.FindAsync(item.ProductId)
            });
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id < 1) BadRequest();
            ProductImage? productImage = await _db.ProductImages.FindAsync(id);
            if (productImage == null) NotFound();

            string fullPath = PathConstants.RootPath + "\\" + productImage.ImageUrl;
            SIO.File.Delete(fullPath);
            _db.ProductImages.Remove(productImage);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id < 1 || id == null) return BadRequest();

            ProductImage item = await _db.ProductImages.FindAsync(id);

            if (item == null) return NotFound();

            List<AdminProductImageListItemVM> listItem =
               await _db.Products.Where(x => !x.IsDeleted).Select(data => new AdminProductImageListItemVM
               {
                   Id = data.Id,
                   ProductName = data.Title
               }).ToListAsync();
            ViewBag.Products = new SelectList(listItem, "Id", "ProductName");


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, AdminUpdateProductImageVM data)
        {
            if (id == null || id < 1) return BadRequest();

            if (data.ProductId == null || data.ProductId < 1) return BadRequest();

            if (!await _db.Products.AnyAsync(x => x.Id == data.ProductId))
                return NotFound();

            var item = await _db.ProductImages.FindAsync(id);

            if (item == null)
                return NotFound();

            if (data.ImgUrl != null)
            {
                if (!data.ImgUrl.IsCorrectType())
                {
                    ModelState.AddModelError("ImgUrl", "Wrong file type");
                }

                if (data.ImgUrl.IsValidSize(20000))
                {
                    ModelState.AddModelError("ImgUrl", "Files length must be less than kb");

                }
            }

            if (!ModelState.IsValid)
            {
                List<AdminProductImageListItemVM> listItem =
             await _db.Products.Where(x => !x.IsDeleted).Select(data => new AdminProductImageListItemVM
             {
                 Id = data.Id,
                 ProductName = data.Title
             }).ToListAsync();
                ViewBag.Products = new SelectList(listItem, "Id", "ProductName");
                return View(data);
            }

            item.ProductId = data.ProductId;
            item.ImageUrl = data.ImgUrl.SaveAsync(PathConstants.ProductImage).Result;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> ProductImagePagination(int page = 1, int count = 3)
        {
            IQueryable<AdminProductImageListItemVM> item = _db.ProductImages.Select(x => new AdminProductImageListItemVM
            {
                Id = x.Id,
                ImageUrls = x.ImageUrl,
                ProductName = x.Product.Title
            }).Skip((page - 1) * count)
            .Take(count);

            int totalCount = await _db.ProductImages.CountAsync();
            PaginatonVM<IEnumerable<AdminProductImageListItemVM>> data = new(totalCount, page, (int)Math.Ceiling((decimal)totalCount / count), item);
            return PartialView("_ProductImagePaginationPartial", data);
        }
    }
}
