﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogLibrary;
using DataAccessLayer.Interfaces;
using System.Threading.Tasks;
using System;
using WebApplication.ViewModels;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace WebApplication.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class BlogPostController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IBlogTagRepository _blogTagRepository;
        private readonly IBlogTagAppliedRepository _blogTagAppliedRepository;
        private readonly IOptions<AppSettings> _appSettings;

        public BlogPostController(IBlogPostRepository blogPostRepository, IAuthorRepository authorRepository, IBlogTagRepository blogTagRepository, IBlogTagAppliedRepository blogTagAppliedRepository, IOptions<AppSettings> appSettings)
        {
            _blogPostRepository = blogPostRepository;
            _authorRepository = authorRepository;
            _blogTagRepository = blogTagRepository;
            _blogTagAppliedRepository = blogTagAppliedRepository;
            _appSettings = appSettings;
        }
        // GET: AuthorController
        public async Task<ActionResult> Index()
        {
            PostListViewModel myPostListViewModel = new PostListViewModel();
            IEnumerable<BlogPost> blogPostsToAdd = await _blogPostRepository.ListAllAsync();
            myPostListViewModel.BlogPosts = blogPostsToAdd.ToList();
            myPostListViewModel.Authors = await _authorRepository.ListAllAsync();
            myPostListViewModel.BlogTags = await _blogTagRepository.ListAllAsync();
            myPostListViewModel.BlogTagsApplied = await _blogTagAppliedRepository.ListAllAsync();
            myPostListViewModel.SiteUrl = _appSettings.Value.SiteUrl;
            return View(myPostListViewModel);
        }

        // GET: AuthorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            PostDetailViewModel myPostDetailViewModel = new PostDetailViewModel();
            myPostDetailViewModel.BlogPost = await _blogPostRepository.GetByIdAsync(id);
            int authorId = myPostDetailViewModel.BlogPost.AuthorId;
            myPostDetailViewModel.Author = await _authorRepository.GetByIdAsync(authorId);
            myPostDetailViewModel.SiteUrl = _appSettings.Value.SiteUrl;     
            List<BlogTag> blogTagsFinalList = new List<BlogTag>();
            List<BlogTagApplied> blogTagsForThisPost = await _blogTagAppliedRepository.GetAllByPostId(id,false);
            foreach (BlogTagApplied blogTagApplied in blogTagsForThisPost)
            {
                blogTagsFinalList.Add(await _blogTagRepository.GetByIdAsync(blogTagApplied.BlogTagId));
            }
            myPostDetailViewModel.BlogTags = blogTagsFinalList;
            return View(myPostDetailViewModel);
        }

        // GET: AuthorController/Create
        public async Task<IActionResult> Create()
        {
            PostEditViewModel myPostEditViewModel = new PostEditViewModel();
            myPostEditViewModel.BlogPost = new BlogPost();
            myPostEditViewModel.Authors = await _authorRepository.ListAllAsync();
            myPostEditViewModel.SiteUrl = _appSettings.Value.SiteUrl;
            IEnumerable<BlogTag> blogTagsToAdd = await _blogTagRepository.ListAllAsync();
            myPostEditViewModel.BlogTags = blogTagsToAdd.ToList();
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
                newBlogPost.PostDate = DateTime.Now;
                newBlogPost.ModifiedDate = DateTime.Now;
                await _blogPostRepository.AddAsync(newBlogPost);
                var editId = newBlogPostModel.BlogPost.BlogPostId;
                return RedirectToAction("Edit", new { id = editId });
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
            IEnumerable<BlogTag> blogTagsToAdd = await _blogTagRepository.ListAllAsync();
            myPostEditViewModel.BlogTags = blogTagsToAdd.ToList();
            myPostEditViewModel.SiteUrl = _appSettings.Value.SiteUrl;
            foreach (BlogTag b in myPostEditViewModel.BlogTags)
            {
                if(await _blogTagAppliedRepository.IsBlogTagApplied(id,b.BlogTagId))
                {
                    b.Checked = true;
                }
            }
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
                editedBlogPost.ModifiedDate = System.DateTime.Now;
                String blogUrlString = editedBlogPost.ImageUrl;
                int startIndex = blogUrlString.IndexOf('=');
                if(startIndex != -1)
                {
                    blogUrlString = blogUrlString.Substring(startIndex + 8);
                    int endIndex = blogUrlString.IndexOf('"');
                    blogUrlString = blogUrlString.Substring(0, endIndex);
                    editedBlogPost.ImageUrl = blogUrlString;
                }
                startIndex = blogUrlString.IndexOf("<p>");
                if (startIndex != -1)
                {
                    blogUrlString = blogUrlString.Substring(startIndex+3);
                    int endIndex = blogUrlString.IndexOf("</p>");
                    blogUrlString = blogUrlString.Substring(0, endIndex);
                    editedBlogPost.ImageUrl = blogUrlString;
                }


                await _blogPostRepository.UpdateAsync(editedBlogPost);
                await _blogTagAppliedRepository.GetAllByPostId(editedBlogPost.BlogPostId, true);
                foreach (BlogTag blogTag in newBlogPostModel.BlogTags)
                {
                    if (blogTag.Checked)
                    {
                        BlogTagApplied newBlogTagApplied = new BlogTagApplied();
                        newBlogTagApplied.BlogTagId = blogTag.BlogTagId;
                        newBlogTagApplied.BlogPostId = editedBlogPost.BlogPostId;
                        await _blogTagAppliedRepository.AddAsync(newBlogTagApplied);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return View(newBlogPostModel);
        }

        // GET: AuthorController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            PostDetailViewModel myPostDetailViewModel = new PostDetailViewModel();
            myPostDetailViewModel.BlogPost = await _blogPostRepository.GetByIdAsync(id);
            int authorId = myPostDetailViewModel.BlogPost.AuthorId;
            myPostDetailViewModel.SiteUrl = _appSettings.Value.SiteUrl;
            myPostDetailViewModel.Author = await _authorRepository.GetByIdAsync(authorId);
            List<BlogTag> blogTagsFinalList = new List<BlogTag>();
            List<BlogTagApplied> blogTagsForThisPost = await _blogTagAppliedRepository.GetAllByPostId(id, false);
            foreach (BlogTagApplied blogTagApplied in blogTagsForThisPost)
            {
                blogTagsFinalList.Add(await _blogTagRepository.GetByIdAsync(blogTagApplied.BlogTagId));
            }
            myPostDetailViewModel.BlogTags = blogTagsFinalList;
            return View(myPostDetailViewModel);
        }

        // POST: AuthorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            var deletedBlogPost = await _blogPostRepository.GetByIdAsync(id);
            String postUrl = "wwwroot/images/" + deletedBlogPost.BlogPostId.ToString() + "/";
            var DirectoryToDelete = Path.Combine(Directory.GetCurrentDirectory(), postUrl);

            try
            {

                Directory.Delete(DirectoryToDelete, true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                // var notDeletedBlogPost = await _blogPostRepository.GetByIdAsync(id);
                //PostDetailViewModel myPostDetailViewModel = new PostDetailViewModel();
            }
            try
            {
                await _blogPostRepository.DeleteAsync(deletedBlogPost);
                await _blogTagAppliedRepository.GetAllByPostId(deletedBlogPost.BlogPostId, true);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return await Delete(id);
            }
        }

    }
}
