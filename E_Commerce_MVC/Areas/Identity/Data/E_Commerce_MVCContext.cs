using E_Commerce_MVC.Areas.Identity.Data;
using E_Commerce_Shared.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_MVC.Areas.Identity.Data;

public class E_Commerce_MVCContext : IdentityDbContext<ApplicationUser>
{
    public E_Commerce_MVCContext(DbContextOptions<E_Commerce_MVCContext> options)
        : base(options)
    {
    }

    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    base.OnModelCreating(builder);
    //    // Customize the ASP.NET Identity model and override the defaults if needed.
    //    // For example, you can rename the ASP.NET Identity table names and more.
    //    // Add your customizations after calling base.OnModelCreating(builder);
    //}
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Star> Stars { get; set; }
    public DbSet<Favourite> Favourites { get; set; }
}
