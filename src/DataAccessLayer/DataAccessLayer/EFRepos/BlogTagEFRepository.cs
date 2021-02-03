using BlogLibrary;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.EFRepos
{
    public class BlogTagEfRepository : EfRepository<BlogTag>, IBlogTagRepository
    {
        public BlogTagEfRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {

        }
    }
}
