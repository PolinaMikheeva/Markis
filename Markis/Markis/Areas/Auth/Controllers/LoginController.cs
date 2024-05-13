using Markis.Areas.Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Markis.Areas.Auth.Controllers
{
    [Area("Auth")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="userManager">Сервис работы с пользователем</param>
        /// <param name="signInManager">Сервис работы с аутентификацией</param>
        public LoginController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="model">Модель</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.UserName, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "home", new
                    {
                        Area = "",
                    });
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        /// <summary>
        /// Выйти из аккаунта
        /// </summary>
        /// <returns>-</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home", new
            {
                Area = ""
            });
        }
    }
}
