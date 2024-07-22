using Markis.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Markis.Persistance.Context
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

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

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.IdentityUser)
                .WithMany()
                .HasForeignKey(c => c.IdentityUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(item => item.ShoppingCart)
                .WithMany(cart => cart.Items)
                .HasForeignKey(item => item.ShoppingCartId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(item => item.Product)
                .WithMany()
                .HasForeignKey(item => item.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
