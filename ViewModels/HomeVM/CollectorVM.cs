using PustokBook.ViewModels.ProductVM;
using PustokBook.ViewModels.SliderVM;

namespace PustokBook.ViewModels.HomeVM
{
    public class CollectorVM
    {
        public List<SliderListItemVm> Sliders { get; set; }
        public List<ProductListItemVM> Products { get; set; }
    }
}
