using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogLibrary
{
    public class BlogTagApplied : BaseEntity
    {
        [Key]
        public int BlogTagAppliedId { get; set; }
        [ForeignKey("BlogPost")]
        public int BlogPostId { get; set; }
        public BlogPost Author { get; set; }
        [ForeignKey ("BlogTag")]
        public int BlogTagId { get; set; }
        public BlogTag BlogTag { get; set; }
    }
}
