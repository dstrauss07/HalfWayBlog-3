using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogLibrary;

namespace WebApplication.ViewModels
{
    public class PostEditViewModel
    {
        public BlogPost BlogPost { get; set; }
        public Author Author { get; set; }
        public IEnumerable<Author> Authors { get; set; }
        public IEnumerable<BlogTag> BlogTags { get; set; }
    }
}
