
using FrontToBack2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack2.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public   DbSet<Slider> Sliders { get; set; }

        public DbSet<SliderDetail> SliderDetails { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Sales> Sales { get; set; }

        public DbSet<SalesProducts> SalesProducts { get; set; }


    }
}
