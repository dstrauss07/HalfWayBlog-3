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
using System;
using WebApplication.Methods;
using Microsoft.Extensions.Options;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBlogTagRepository _blogTagRepository;
        private readonly IBlogTagAppliedRepository _blogTagAppliedRepository;
        private readonly IOptions<AppSettings> _appSettings;
        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository, IAuthorRepository authorRepository, IBlogTagRepository blogTagRepository, IBlogTagAppliedRepository blogTagAppliedRepository, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _blogPostRepository = blogPostRepository;
            _authorRepository = authorRepository;
            _blogTagRepository = blogTagRepository;
            _blogTagAppliedRepository = blogTagAppliedRepository;
            _appSettings = appSettings;
        }

        public async Task<ActionResult> Index(int tagId,int pageNum,string searchString)
        {
            HomeViewModel myHomeViewModel = new HomeViewModel();
            IEnumerable<BlogPost> blogPostsToAdd = await _blogPostRepository.ListAllAsync();
            myHomeViewModel.BlogPosts = blogPostsToAdd.ToList();
            myHomeViewModel.Authors = await _authorRepository.ListAllAsync();
            myHomeViewModel.BlogTags = await _blogTagRepository.ListAllAsync();
            myHomeViewModel.BlogTagsApplied = await _blogTagAppliedRepository.ListAllAsync();
            myHomeViewModel.SiteUrl = _appSettings.Value.SiteUrl;
            myHomeViewModel.PageNum = pageNum;
            myHomeViewModel.BlogPosts.Reverse();
            if (searchString != null)
            {
                myHomeViewModel.BlogTag = null;
                myHomeViewModel.BlogPosts = SearchPosts.GetPostsBySearchString(searchString, myHomeViewModel);
                myHomeViewModel.searchString = searchString;
            }
            else if (tagId == 0 )
            {
                myHomeViewModel.BlogTag = null;
            }
            else
            {
                GetPostsByTag(tagId, myHomeViewModel);
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
            myHomeViewModel.SiteUrl = _appSettings.Value.SiteUrl;
            myHomeViewModel.BlogPosts.Reverse();
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

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private static void GetPostsByTag(int tagId, HomeViewModel myHomeViewModel)
        {
            List<BlogTag> allBlogTags = myHomeViewModel.BlogTags.ToList();
            myHomeViewModel.BlogTag = allBlogTags.Find(x => x.BlogTagId == tagId);
            List<BlogPost> updatedBlogPostList = new List<BlogPost>();
            foreach (BlogTagApplied bta in myHomeViewModel.BlogTagsApplied)
            {
                if (bta.BlogTagId == tagId)
                {
                    updatedBlogPostList.Add(myHomeViewModel.BlogPosts.Find(x => x.BlogPostId == bta.BlogPostId));
                }
            }
            myHomeViewModel.BlogPosts = updatedBlogPostList;
        }

    }
}
