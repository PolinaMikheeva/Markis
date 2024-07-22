using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Markis.Domain.Entities;
using Markis.Persistance.Context;
using Markis.Application.Services.Payment;

namespace Markis.Areas.Shopping.Controllers
{
    [Area("Shopping")]
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var cart = await _context.ShoppingCarts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            var cart = await _context.ShoppingCarts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new ShoppingCart
                {
                    UserId = userId,
                    Items = new List<ShoppingCartItem>()
                };
                _context.ShoppingCarts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem == null)
            {
                cartItem = new ShoppingCartItem
                {
                    ShoppingCartId = cart.Id,
                    ProductId = productId,
                    Quantity = 1
                };
                _context.ShoppingCartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity++;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home", new { area = "Shopping" });
        }



        [HttpPost]
        public async Task<IActionResult> Buy()
        {
            var userId = _userManager.GetUserId(User);
            var cart = await _context.ShoppingCarts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.Items.Any())
            {
                return BadRequest("Your cart is empty.");
            }

            var buyer = await _context.UserProfiles.FirstOrDefaultAsync(u => u.IdentityUserId == userId);

            foreach (var item in cart.Items)
            {
                var product = item.Product;
                var seller = await _context.UserProfiles.FirstOrDefaultAsync(u => u.IdentityUserId == product.IdentityUserId);

                if (buyer == null || seller == null)
                {
                    return BadRequest("Invalid user.");
                }

                if (buyer.Balance < item.Product.Price * item.Quantity)
                {
                    return BadRequest("Insufficient funds.");
                }

                buyer.Balance -= item.Product.Price * item.Quantity;

                var paymentService = new PaymentService(_context);
                paymentService.ProcessPayment(seller, item.Product.Price * item.Quantity);
            }

            _context.ShoppingCartItems.RemoveRange(cart.Items);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home", new { area = "Shopping" });
        }

    }
}
