using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogLibrary;
using DataAccessLayer.Interfaces;
using System.Threading.Tasks;
using System;

namespace WebApplication.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }
        // GET: AuthorController
        public async Task<ActionResult> Index()
        {
            var listOfBlogPosts = await _blogPostRepository.ListAllAsync();
            return View(listOfBlogPosts);
        }

        // GET: AuthorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var blogPostDetails = await _blogPostRepository.GetByIdAsync(id);
            return View(blogPostDetails);
        }

        // GET: AuthorController/Create
        public ActionResult Create()
        {
            return View(new BlogPost());
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost newBlogPost, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newBlogPost);
                }
                await _blogPostRepository.AddAsync(newBlogPost);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(newBlogPost);
        }

        // GET: AuthorController/Edit/5

        public async Task<IActionResult> Edit(int id)
        {
            var blogPostToEdit = await _blogPostRepository.GetByIdAsync(id);
            return View(blogPostToEdit);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogPost editedBlogPost, IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(editedBlogPost);
            }
            try
            {
                await _blogPostRepository.UpdateAsync(editedBlogPost);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //todo log exception
            }
            return View(editedBlogPost);
        }

        // GET: AuthorController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var blogPostToDelete = await _blogPostRepository.GetByIdAsync(id);
            return View(blogPostToDelete);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                var deletedBlogPost = await _blogPostRepository.GetByIdAsync(id);
                await _blogPostRepository.DeleteAsync(deletedBlogPost);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var notDeletedBlogPost = await _blogPostRepository.GetByIdAsync(id);
                return View(notDeletedBlogPost);
            }
        }
    }
}
