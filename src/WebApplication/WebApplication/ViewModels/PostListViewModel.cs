﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogLibrary;

namespace WebApplication.ViewModels
{
    public class PostListViewModel
    {
        public List<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<BlogTag> BlogTags { get; set; }
        public IEnumerable<BlogTagApplied> BlogTagsApplied { get; set; }
        public string SiteUrl { get; set; }
    }
}
