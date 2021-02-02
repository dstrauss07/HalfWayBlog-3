using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogLibrary
{
    public class BlogTag : BaseEntity
    {
        [Key]
        public int BlogTagId { get; set; }
        public string BlogTagName { get; set; }
        [ForeignKey("BlogPost")]
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
