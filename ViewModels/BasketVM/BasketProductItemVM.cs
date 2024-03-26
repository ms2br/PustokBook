namespace PustokBook.ViewModels.BasketVM
{
    public class BasketProductItemVM
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string ActiveImgHover { get; set; }
        public byte Discount { get; set; }
    }
}
