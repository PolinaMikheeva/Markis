using Microsoft.AspNetCore.Mvc;
using Markis.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Markis.Areas.Admin.Models.AdminPost;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Markis.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AdminPostController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminPostController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ActionResult> Index()
        {
            var data = await _context.Posts.Select(x => new AdminPostFetchVM()
            {
                Id = x.Id,
                Title = x.Title,
                ShortDescription = x.ShortDescription,
                Description = x.ShortDescription,
                ReleaseDate = x.ReleaseDate,
                UserId = x.UserId,
                UserName = x.User.Username
            }).ToListAsync();

            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            return View(post);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(AdminPostFetchVM postVM)
        {
            var currentPost = await _context.Posts.FindAsync(postVM.Id);

            if (currentPost is not null)
            {
                currentPost.Id = postVM.Id;
                currentPost.Title = postVM.Title;
                currentPost.ShortDescription = postVM.ShortDescription;
                currentPost.Description = postVM.Description;
                currentPost.ReleaseDate = postVM.ReleaseDate;
                currentPost.UserId = postVM.UserId;

                await _context.SaveChangesAsync();
            }


            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AdminPostFetchVM postVM)
        {
            var postDelete = await _context.Posts.FirstOrDefaultAsync(s => s.Id == postVM.Id);

            if (postDelete is not null)
            {
                _context.Posts.Remove(postDelete);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
