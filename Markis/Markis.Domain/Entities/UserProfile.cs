using Microsoft.AspNetCore.Identity;

namespace Markis.Domain.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        public List<Post> Posts { get; set; }

        public List<Product> Products { get; set; }
    }
}
