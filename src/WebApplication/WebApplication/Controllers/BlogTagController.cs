using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogLibrary;
using DataAccessLayer.Interfaces;
using System.Threading.Tasks;
using System;

namespace WebApplication.Controllers
{
    public class BlogTagController : Controller
    {
        private readonly IBlogTagRepository _blogTagRepository;

        public BlogTagController(IBlogTagRepository blogTagRepository)
        {
            _blogTagRepository = blogTagRepository;
        }
        // GET: AuthorController
        public async Task<ActionResult> Index()
        {
            var listOfBlogTags = await _blogTagRepository.ListAllAsync();
            return View(listOfBlogTags);
        }

        // GET: AuthorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var blogTagDetails = await _blogTagRepository.GetByIdAsync(id);
            return View(blogTagDetails);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View(new BlogTag());
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogTag newBlogTag, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newBlogTag);
                }
                await _blogTagRepository.AddAsync(newBlogTag);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(newBlogTag);
        }

        // GET: AuthorController/Edit/5

        public async Task<IActionResult> Edit(int id)
        {
            var blogTagToEdit = await _blogTagRepository.GetByIdAsync(id);
            return View(blogTagToEdit);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogTag editedBlogTag, IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(editedBlogTag);
            }
            try
            {
                await _blogTagRepository.UpdateAsync(editedBlogTag);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //todo log exception
            }
            return View(editedBlogTag);
        }

        // GET: AuthorController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var blogTagToDelete = await _blogTagRepository.GetByIdAsync(id);
            return View(blogTagToDelete);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var deletedBlogTag = await _blogTagRepository.GetByIdAsync(id);
                await _blogTagRepository.DeleteAsync(deletedBlogTag);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var notDeletedBlogTag = await _blogTagRepository.GetByIdAsync(id);
                return View(notDeletedBlogTag);
            }
        }
    }
}
