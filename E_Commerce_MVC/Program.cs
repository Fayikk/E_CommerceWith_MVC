using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using E_Commerce_MVC.Areas.Identity.Data;
using E_Commerce_MVC.Services.Abstract;
using E_Commerce_MVC.Services.Concrete;
using E_Commerce_MVC.Extensions;
using E_Commerce_MVC.Core.ForEmail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("E_Commerce_MVCContextConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<E_Commerce_MVCContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<E_Commerce_MVCContext>();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "E_CommerceWith_MVC";
    options.IdleTimeout = TimeSpan.FromDays(1);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddDefaultTokenProviders().AddDefaultUI().AddEntityFrameworkStores<E_Commerce_MVCContext>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IFilterProductService, FilterProductService>();
builder.Services.AddScoped<IEmailService, EmailService>();  
builder.Services.AddScoped<IStarService, StarService>();
builder.Services.AddScoped<IFavouriteService, FavouriteService>();
builder.Services.AddControllersWithViews();
builder.Services.AddElastic(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ProductClient}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
