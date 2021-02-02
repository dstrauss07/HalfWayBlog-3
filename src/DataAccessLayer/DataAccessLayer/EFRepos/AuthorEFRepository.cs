using BlogLibrary;
using DataAccessLayer.Interfaces;

namespace DataAccessLayer.EFRepos
{
    public class AuthorEFRepository : EfRepository<Author>, IAuthorRepository
    {
        public AuthorEFRepository(BlogDbContext blogDbContext) : base(blogDbContext)
        {

        }
    }
}
