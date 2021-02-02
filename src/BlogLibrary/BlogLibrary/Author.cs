using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace BlogLibrary
{
    public class Author : BaseEntity
    {
        [Key]
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public IEnumerable<BlogPost> BlogPosts { get; set; }
    }
}
