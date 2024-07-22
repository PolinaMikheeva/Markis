using Markis.Application.Mappings;
using Markis.Application.Repositories.Comments;
using Markis.Application.Repositories.Posts;
using Markis.Application.Repositories.Products;
using Markis.Application.Repositories.Users;
using Markis.Application.Services.Comments;
using Markis.Application.Services.Posts;
using Markis.Application.Services.Products;
using Markis.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Markis.Application.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PostProfile));
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<UserProfileService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();


        }
    }
}
