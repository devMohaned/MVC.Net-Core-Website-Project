using Final_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public  DbSet<Brand> Brands { get; set; }
        public  DbSet<Category> Categories { get; set; }
        public  DbSet<SubCategory>  SubCategories { get; set; }
        public  DbSet<Favorite> Favorites { get; set; }
        public  DbSet<Ad> Ads { get; set; }
        public  DbSet<AdImage> AdImages { get; set; }
        public  DbSet<Question> Questions { get; set; }
        public  DbSet<User>  Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Favorite>().HasKey(x => new { x.CustomerId, x.AdtId });
            base.OnModelCreating(builder);
        }
    }
}