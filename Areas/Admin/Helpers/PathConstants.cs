namespace PustokBook.Areas.Admin.Helpers
{
    public static class PathConstants
    {
        public static string ProductSlider => Path.Combine("assets", "img", "slider");
        public static string ProductImage => Path.Combine("assets", "img", "product");

        public static string ProfilImage => Path.Combine("assets", "img", "profil");

        public static string RootPath { get; set; }
    }
}
