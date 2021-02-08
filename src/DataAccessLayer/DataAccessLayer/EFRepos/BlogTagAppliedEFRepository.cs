using BlogLibrary;
using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.EFRepos
{
    public class BlogTagAppliedEfRepository : EfRepository<BlogTagApplied>, IBlogTagAppliedRepository
    {
        public BlogTagAppliedEfRepository (BlogDbContext blogDbContext) : base(blogDbContext)
        {

        }

        public async Task<List<BlogTagApplied>> GetAllByPostId(int id, bool delete)
        {
            List<BlogTagApplied> blogTagAppliedToReturnList = new List<BlogTagApplied>();
            IReadOnlyList<BlogTagApplied> allBlogTagsApplied = await ListAllAsync();
            foreach(BlogTagApplied b in allBlogTagsApplied)
            {
                if(b.BlogPostId == id)
                {
                    if(delete)
                    {
                        await DeleteAsync(b);
                    }
                    else
                    {
                        blogTagAppliedToReturnList.Add(b);
                    }
                }
            }
            if(!delete)
            {
                return blogTagAppliedToReturnList;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> IsBlogTagApplied(int blogId, int tagId)
        {
            IReadOnlyList<BlogTagApplied> allBlogTagsApplied = await ListAllAsync();
            foreach (BlogTagApplied b in allBlogTagsApplied)
            {
                if(b.BlogTagId == tagId)
                {
                    return true;
                }
            }
                return false;
        }
    }
}
