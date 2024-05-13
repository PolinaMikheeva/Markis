using Markis.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Markis.Persistance.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>().HasMany(p => p.PostTags)
                .WithMany(p => p.Posts)
                .UsingEntity(p => p.ToTable("PostPostTags"));

            modelBuilder.Entity<Product>().HasMany(p => p.ProductTags)
                .WithMany(p => p.Products)
                .UsingEntity(p => p.ToTable("ProductProductTags"));
        }
    }
}
