using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBook.Areas.Admin.Helpers;
using PustokBook.Contexts;
using PustokBook.ViewModels.CommonVM;
using PustokBook.ViewModels.ProductVM;
namespace PustokBook.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        public PustokDbContexts _db { get; set; }

        public ProductViewComponent(PustokDbContexts db)
        {
            _db = db;
        }


        public async Task<IViewComponentResult> InvokeAsync(int page = 1, int count = 3)
        {
            List<ProductListItemVM> products = await _db.Products.Select(x => new ProductListItemVM
            {
                ActiveImage = x.ActiveImage,
                Authors = x.AuthorBooks.Select(x => x.Author),
                SellPrice = x.SellPrice,
                Discount = x.Discount,
                Quantity = x.Quantity,
                ProductCode = x.ProductCode,
                Id = x.Id,
                CategoryId = x.CategoryId,
                Description = x.Description,
                Title = x.Title,
                Category = x.Category,
                Tags = x.ProductTags.Select(x => x.Tag)
            }).Paginaton(page, count).ToListAsync();

            int totalCount = await _db.Products.CountAsync();

            PaginationVM<List<ProductListItemVM>> paginationVM = new PaginationVM<List<ProductListItemVM>>(totalCount, page, (int)Math.Ceiling((decimal)totalCount / count), products);
            return View(paginationVM);
        }
    }
}
