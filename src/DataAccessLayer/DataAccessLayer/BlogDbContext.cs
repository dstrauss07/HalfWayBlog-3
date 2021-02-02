using BlogLibrary;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class BlogDbContext : DbContext
    {

        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }
        public virtual DbSet<Author> Author { get; set; }
        public virtual DbSet<BlogPost> BlogPost { get; set; }
        public virtual DbSet<BlogTag> BlogTag { get; set; }
    }
}

