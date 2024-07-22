using Microsoft.AspNetCore.Identity;

namespace Markis.Domain.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public IdentityUser User { get; set; }

        public ICollection<ShoppingCartItem> Items { get; set; }
    }
}
