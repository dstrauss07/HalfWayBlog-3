using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogLibrary;

namespace WebApplication.ViewModels
{
    public class HomeViewModel
    {
        public BlogPost BlogPost { get; set; }
        public int BlogIndex { get; set; }
        public List<BlogPost> BlogPosts { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<BlogTag> BlogTags { get; set; }
        public IEnumerable<BlogTagApplied> BlogTagsApplied { get; set; }
        public BlogTag BlogTag { get; set; }
        public int PageNum { get; set; }
        public String searchString { get; set; }
    }
}
