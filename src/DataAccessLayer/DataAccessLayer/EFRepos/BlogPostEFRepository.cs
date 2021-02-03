using BlogLibrary;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.EFRepos
{
    public class BlogPostEFRepository : EfRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostEFRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {

        }
    }
}
