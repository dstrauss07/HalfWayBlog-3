using BlogLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
   public interface IBlogTagAppliedRepository : IAsyncRepository<BlogTagApplied>
    {
        Task<List<BlogTagApplied>> GetAllByPostId(int id, bool delete);

        Task<bool> IsBlogTagApplied(int blogId, int tagId);
    }
}
