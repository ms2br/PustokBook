using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokBook.Areas.Admin.Helpers;
using PustokBook.Areas.Admin.ViewModels.CommonVM;
using PustokBook.Areas.Admin.ViewModels.ProductVM;
using PustokBook.Contexts;

namespace PustokBook.Areas.Admin.ViewComponents
{
    public class ProductPaginationViewComponent : ViewComponent
    {
        public PustokDbContexts _db { get; }

        public ProductPaginationViewComponent(PustokDbContexts db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(int page = 1, int count = 3)
        {
            var item = _db.Products.Select(x => new AdminProductListItemVM
            {
                Id = x.Id,
                Authors = x.AuthorBooks.Select(x => x.Author),
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
            }).Paginaton(page, count);
            int totalCount = await _db.Products.Where(x => !x.IsDeleted).CountAsync();
            PaginatonVM<IQueryable<AdminProductListItemVM>> paginaton = new(totalCount, page, (int)Math.Ceiling((decimal)totalCount / count), item);
            return View(paginaton);
        }
    }
}
