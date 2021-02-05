using BlogLibrary;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.EFRepos
{
    public class BlogTagAppliedEfRepository : EfRepository<BlogTagApplied>, IBlogTagAppliedRepository
    {
        public BlogTagAppliedEfRepository (BlogDbContext blogDbContext) : base(blogDbContext)
        {

        }
    }
}
