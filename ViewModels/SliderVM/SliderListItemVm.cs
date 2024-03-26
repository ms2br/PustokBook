namespace PustokBook.ViewModels.SliderVM
{
    public record SliderListItemVm
    {
        public string AuthorName { get; init; }

        public string BookName { get; init; }

        public byte Position { get; init; }

        public string ImageUrl { get; init; }
    }
}
