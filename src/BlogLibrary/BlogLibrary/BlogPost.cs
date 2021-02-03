﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogLibrary
{
    public class BlogPost : BaseEntity
    {
        [Key]
        public int BlogPostId { get; set; }
        public string BlogPostTitle { get; set; }
        public string BlogText { get; set; }
        public IEnumerable<BlogTag> BlogTags { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

    }
}