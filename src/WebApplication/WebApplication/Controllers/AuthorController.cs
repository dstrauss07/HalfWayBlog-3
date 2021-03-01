using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogLibrary;
using DataAccessLayer.Interfaces;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Administrator")]
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
        public async Task<IActionResult> Create(Author newAuthor, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newAuthor);
                }
                await _authorRepository.AddAsync(newAuthor);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(newAuthor);
        }

        // GET: AuthorController/Edit/5

        public async Task<IActionResult> Edit(int id)
        {
            var authorToEdit = await _authorRepository.GetByIdAsync(id);
            return View(authorToEdit);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Author editedAuthor, IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(editedAuthor);
            }
            try
            {
                await _authorRepository.UpdateAsync(editedAuthor);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //todo log exception
            }
            return View(editedAuthor);
        }

        // GET: AuthorController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var authorToDelete = await _authorRepository.GetByIdAsync(id);
            return View(authorToDelete);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var deletedAuthor = await _authorRepository.GetByIdAsync(id);
                await _authorRepository.DeleteAsync(deletedAuthor);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var notDeletedAuthor = await _authorRepository.GetByIdAsync(id);
                return View(notDeletedAuthor);
            }
        }
    }
}
