using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PustokBook.Contexts;
using PustokBook.ViewModels.BasketVM;

namespace PustokBook.ViewComponents
{
    public class BasketViewComponent : ViewComponent
    {
        PustokDbContexts _db { get; }
        //IHttpContextAccessor _hc { get; }
        public BasketViewComponent(PustokDbContexts db/*IHttpContextAccessor hc*/)
        {
            _db = db;
            //_hc = hc;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = JsonConvert.DeserializeObject<List<BasketProductVMAndCountVM>>(HttpContext.Request.Cookies["basket"] ?? "[]");
            var products = _db.Products.Where(x => items.Select(m => m.Id).Contains(x.Id));
            List<BasketProductItemVM> basketItems = new();
            foreach (var item in products)
            {
                basketItems.Add(new BasketProductItemVM
                {
                    Id = item.Id,
                    ActiveImgHover = item.ActiveImage,
                    Count = items.SingleOrDefault(x => x.Id == item.Id).Count,
                    Discount = (byte)item.Discount,
                    Name = item.Title,
                    Price = (float)item.SellPrice
                });
            }
            return View(basketItems);
        }

    }
}
