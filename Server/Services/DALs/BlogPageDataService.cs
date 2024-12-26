using Microsoft.EntityFrameworkCore;
using Server.Data;
using SharedClasses.Interfaces;
using SharedClasses.Models.BlogModels;

namespace Server.Services.DALs
{
    public class BlogPageDataService(ApplicationDbContext context) : IBlogPageDataService
    {
        public async Task<Blog?> GetBlogFromRouteAsync(string route)
        {
            Blog? blog = await context.Blogs.Where(b => b.Route != null && b.Route.ToLower() == route.ToLower()).Include(b=>b.Posts!).FirstOrDefaultAsync();
            return blog;
        }
    }
}
