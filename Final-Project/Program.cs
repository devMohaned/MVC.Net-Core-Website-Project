using Final_Project.Data;
using Final_Project.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));//.UseLazyLoadingProxies()


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<User,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddDefaultUI().AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddTransient<IBrandService, BrandService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IFavoriteService, FavoriteService>();
builder.Services.AddTransient<IAdImageService, AdImageService>();
builder.Services.AddTransient<IAdService, AdtService>();
builder.Services.AddTransient<IQuestionService, QuestionService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISubCategoryServicecs, SubCategoryService>();
builder.Services.AddControllersWithViews();

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
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/Error";
        await next();
    }
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
