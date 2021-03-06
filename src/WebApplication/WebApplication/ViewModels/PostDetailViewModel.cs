﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogLibrary;

namespace WebApplication.ViewModels
{
    public class PostDetailViewModel
    {
        public BlogPost BlogPost { get; set; }
        public Author Author { get; set; }
        public List<BlogTag> BlogTags { get; set; }
        public string SiteUrl { get; set; }
    }
}
