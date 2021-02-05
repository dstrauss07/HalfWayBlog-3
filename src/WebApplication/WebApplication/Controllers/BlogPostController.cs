using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogLibrary;
using DataAccessLayer.Interfaces;
using System.Threading.Tasks;
using System;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBlogTagRepository _blogTagRepository;
        private readonly IBlogTagAppliedRepository _blogTagAppliedRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository, IAuthorRepository authorRepository, IBlogTagRepository blogTagRepository, IBlogTagAppliedRepository blogTagAppliedRepository)
        {
            _blogPostRepository = blogPostRepository;
            _authorRepository = authorRepository;
            _blogTagRepository = blogTagRepository;
            _blogTagAppliedRepository = blogTagAppliedRepository;
        }
        // GET: AuthorController
        public async Task<ActionResult> Index()
        {
            PostListViewModel myPostListViewModel = new PostListViewModel();
            myPostListViewModel.BlogPosts = await _blogPostRepository.ListAllAsync();
            myPostListViewModel.Authors = await _authorRepository.ListAllAsync();
            myPostListViewModel.BlogTags = await _blogTagRepository.ListAllAsync();

            return View(myPostListViewModel);
        }

        // GET: AuthorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            PostDetailViewModel myPostDetailViewModel = new PostDetailViewModel();
            myPostDetailViewModel.BlogPost = await _blogPostRepository.GetByIdAsync(id);
            int authorId = myPostDetailViewModel.BlogPost.AuthorId;
            myPostDetailViewModel.Author = await _authorRepository.GetByIdAsync(authorId);
            //TODO may redo this after reworking Tags
            myPostDetailViewModel.BlogTags = await _blogTagRepository.ListAllAsync();
            return View(myPostDetailViewModel);
        }

        // GET: AuthorController/Create
        public async Task<IActionResult> Create()
        {
            PostEditViewModel myPostEditViewModel = new PostEditViewModel();
            myPostEditViewModel.BlogPost = new BlogPost();
            myPostEditViewModel.Authors = await _authorRepository.ListAllAsync();
            myPostEditViewModel.BlogTags = await _blogTagRepository.ListAllAsync();
     //       myPostEditViewModel.BlogTagsApplied = await _blogTagAppliedRepository.ListAllAsync();
            return View(myPostEditViewModel);
        }

        // POST: AuthorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostEditViewModel newBlogPostModel, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newBlogPostModel);
                }
                BlogPost newBlogPost = newBlogPostModel.BlogPost;
                await _blogPostRepository.AddAsync(newBlogPost);
                foreach(BlogTag blogTag in newBlogPostModel.BlogTags)
                {
                    Console.WriteLine(blogTag);
                }

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return View(newBlogPostModel);
        }

        // GET: AuthorController/Edit/5

        public async Task<IActionResult> Edit(int id)
        {
            PostEditViewModel myPostEditViewModel = new PostEditViewModel();
            myPostEditViewModel.Authors = await _authorRepository.ListAllAsync();
            myPostEditViewModel.BlogTags = await _blogTagRepository.ListAllAsync();
            myPostEditViewModel.BlogPost = await _blogPostRepository.GetByIdAsync(id);
            return View(myPostEditViewModel);
        }

        // POST: AuthorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PostEditViewModel newBlogPostModel, IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(newBlogPostModel);
            }
            try
            {
                BlogPost editedBlogPost = newBlogPostModel.BlogPost;
                await _blogPostRepository.UpdateAsync(editedBlogPost);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                //todo log exception
            }
            return View(newBlogPostModel);
        }

        // GET: AuthorController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            PostDetailViewModel myPostDetailViewModel = new PostDetailViewModel();
            myPostDetailViewModel.BlogPost = await _blogPostRepository.GetByIdAsync(id);
            int authorId = myPostDetailViewModel.BlogPost.AuthorId;
            myPostDetailViewModel.Author = await _authorRepository.GetByIdAsync(authorId);
            //TODO may redo this after reworking Tags
            myPostDetailViewModel.BlogTags = await _blogTagRepository.ListAllAsync();
            return View(myPostDetailViewModel);
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
