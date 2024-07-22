using Markis.Domain.Entities;
using Markis.Persistance.Context;

namespace Markis.Application.Services.Payment
{
    public class PaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void ProcessPayment(UserProfile seller, decimal amount)
        {
            seller.Balance += amount;

            _context.SaveChanges();
        }
    }
}
