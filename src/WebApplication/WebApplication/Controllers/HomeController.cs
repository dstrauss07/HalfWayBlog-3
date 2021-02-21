using BlogLibrary;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBlogTagRepository _blogTagRepository;
        private readonly IBlogTagAppliedRepository _blogTagAppliedRepository;
        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository, IAuthorRepository authorRepository, IBlogTagRepository blogTagRepository, IBlogTagAppliedRepository blogTagAppliedRepository)
        {
            _logger = logger;
            _blogPostRepository = blogPostRepository;
            _authorRepository = authorRepository;
            _blogTagRepository = blogTagRepository;
            _blogTagAppliedRepository = blogTagAppliedRepository;
        }

        public async Task<ActionResult> Index(int id = 0)
        {
            HomeViewModel myHomeViewModel = new HomeViewModel();
            IEnumerable<BlogPost> blogPostsToAdd = await _blogPostRepository.ListAllAsync();
            myHomeViewModel.BlogPosts = blogPostsToAdd.ToList();
            myHomeViewModel.Authors = await _authorRepository.ListAllAsync();
            myHomeViewModel.BlogTags = await _blogTagRepository.ListAllAsync();
            myHomeViewModel.BlogTagsApplied = await _blogTagAppliedRepository.ListAllAsync();
            if (myHomeViewModel.BlogPosts.Count == 0)
            {
                BlogPost emptyBlogPost = new BlogPost();
                myHomeViewModel.BlogPosts.Add(emptyBlogPost);
            }
            if (id == 0)
            {
                myHomeViewModel.BlogPost = myHomeViewModel.BlogPosts[0];
                myHomeViewModel.BlogIndex = 0;
            }
            else
            {
                myHomeViewModel.BlogPost = myHomeViewModel.BlogPosts.FirstOrDefault(item => item.BlogPostId == id);
                myHomeViewModel.BlogIndex = myHomeViewModel.BlogPosts.FindIndex(item => item.BlogPostId == id);
            }

            return View(myHomeViewModel);
        }

        public async Task<ActionResult> Details(int id = 0)
        {
            HomeViewModel myHomeViewModel = new HomeViewModel();
            IEnumerable<BlogPost> blogPostsToAdd = await _blogPostRepository.ListAllAsync();
            myHomeViewModel.BlogPosts = blogPostsToAdd.ToList();
            myHomeViewModel.Authors = await _authorRepository.ListAllAsync();
            myHomeViewModel.BlogTags = await _blogTagRepository.ListAllAsync();
            myHomeViewModel.BlogTagsApplied = await _blogTagAppliedRepository.ListAllAsync();
            if (id == 0)
            {
                myHomeViewModel.BlogPost = myHomeViewModel.BlogPosts[0];
                myHomeViewModel.BlogIndex = 0;
            }
            else
            {
                myHomeViewModel.BlogPost = myHomeViewModel.BlogPosts.FirstOrDefault(item => item.BlogPostId == id);
                myHomeViewModel.BlogIndex = myHomeViewModel.BlogPosts.FindIndex(item => item.BlogPostId == id);
            }

            return View(myHomeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
