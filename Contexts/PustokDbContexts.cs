using Microsoft.EntityFrameworkCore;
using PustokBook.Models;

namespace PustokBook.Contexts
{
    public class PustokDbContexts : DbContext
    {
        public PustokDbContexts(DbContextOptions db) : base(db) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
