using System.Collections.Generic;
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
        public string ImageUrl { get; set; }
        public System.DateTime PostDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public IEnumerable<BlogTagApplied> BlogTags { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

    }
}
