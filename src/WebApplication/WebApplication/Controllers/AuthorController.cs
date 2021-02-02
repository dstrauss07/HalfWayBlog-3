using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogLibrary;
using DataAccessLayer.Interfaces;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        // GET: AuthorController
        public async Task<ActionResult> Index()
        {
            var listOfAuthors = await _authorRepository.ListAllAsync();
            return View(listOfAuthors);
        }

        // GET: AuthorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var authorDetails = await _authorRepository.GetByIdAsync(id);
            return View(authorDetails);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View(new Author());
        }

        // POST: AuthorController/Create
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

        // GET: AuthorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuthorController/Edit/5
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

        // GET: AuthorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuthorController/Delete/5
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
