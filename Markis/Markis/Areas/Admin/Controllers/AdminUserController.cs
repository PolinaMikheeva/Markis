using Markis.Areas.Admin.Models.AdminPost;
using Markis.Areas.Admin.Models.AdminUser;
using Markis.Persistance.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Markis.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AdminUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var data = await _context.UserProfiles.Select(x => new AdminUserFetchVM()
            {
                Id = x.Id,
                Username = x.Username,
                IdentityUserId = x.IdentityUserId
            }).ToListAsync();

            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var user = await _context.UserProfiles.FindAsync(id);

            return View(user);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(AdminUserFetchVM userVM)
        {
            var currentUser = await _context.UserProfiles.FindAsync(userVM.Id);

            if (currentUser is not null)
            {
                currentUser.Id = userVM.Id;
                currentUser.IdentityUserId = userVM.IdentityUserId;
                currentUser.Username = userVM.Username;

                await _context.SaveChangesAsync();
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AdminUserFetchVM userVM)
        {
            var userDelete = await _context.UserProfiles.FirstOrDefaultAsync(s => s.Id == userVM.Id);

            if (userDelete is not null)
            {
                _context.UserProfiles.Remove(userDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
