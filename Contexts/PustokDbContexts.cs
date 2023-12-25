using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PustokBook.Models;

namespace PustokBook.Contexts
{
    public class PustokDbContexts : IdentityDbContext
    {
        public PustokDbContexts(DbContextOptions db) : base(db) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<AuthorProduct> AuthorBooks { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ProducTag> ProducTags { get; set; }
    }
}
