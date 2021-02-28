using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.ViewModels;
using BlogLibrary;
using System.Text.RegularExpressions;

namespace WebApplication.Methods
{
    public static class SearchPosts
    {

        public static List<BlogPost> GetPostsBySearchString(string searchString, HomeViewModel myHomeViewModel)
        {
            List<BlogPost> blogPostForSearchString = new List<BlogPost>();
            List<BlogTag> relatedBlogTags = new List<BlogTag>();
            string[] searchStrings = searchString.ToLower().Split();

            foreach(string str in searchStrings)
            {
                foreach (BlogTag bt in myHomeViewModel.BlogTags)
                {
                    if (bt.BlogTagName.ToLower().Contains(str))
                    {
                        relatedBlogTags.Add(bt);
                    }
                }
                foreach (BlogTag btr in relatedBlogTags)
                {
                    foreach (BlogTagApplied bta in myHomeViewModel.BlogTagsApplied)
                    {
                        if (btr.BlogTagId == bta.BlogTagId)
                        {
                            foreach (BlogPost bp in myHomeViewModel.BlogPosts)
                            {
                                if (bp.BlogPostId == bta.BlogPostId)
                                {
                                    blogPostForSearchString.Add(bp);
                                }
                            }
                        }
                    }
                }
                foreach (BlogPost bp in myHomeViewModel.BlogPosts)
                {
                    if (bp.BlogPostTitle.ToLower().Contains(str))
                    {
                        blogPostForSearchString.Add(bp);
                    }
                    else if (bp.Author.AuthorName.ToLower().Contains(str))
                    {
                        blogPostForSearchString.Add(bp);
                    }
                    else if (bp.BlogText.ToLower().Contains(str))
                    {
                        blogPostForSearchString.Add(bp);
                    }
                }
            }
            blogPostForSearchString = blogPostForSearchString.Distinct().ToList();
            return blogPostForSearchString;
        }


    }
}
