using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Markis.Controllers
{
    public class AuthorProfileController : Controller
    {
        // GET: AuthorProfileController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AuthorProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AuthorProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthorProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthorProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
