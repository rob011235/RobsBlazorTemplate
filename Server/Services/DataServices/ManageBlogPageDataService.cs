using Microsoft.EntityFrameworkCore;
using Server.Data;
using SharedClasses.Models.BlogModels;

namespace Server.Services.DataServices
{
    public class ManageBlogPageDataService(ApplicationDbContext context)
    {
        public async Task<Blog?> GetBlogByRouteAsync(string route)
        {
            return await context.Blogs.Where(b => b.Route == route).Include(b=>b.Posts).FirstOrDefaultAsync();
        }
    }
}
