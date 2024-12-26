using Microsoft.EntityFrameworkCore;
using Server.Data;
using SharedClasses.Interfaces;
using SharedClasses.Models.BlogModels;

namespace Server.Services.DALs
{
    public class PostPageDAL(ApplicationDbContext context) : IPostPageDAL
    {
        public async Task<Post?> GetPostFromRouteAsync(string route)
        {
            Post? post = await context.Posts.Where(p => p.Route != null && p.Route.ToLower() == route.ToLower()).Include(p => p.Blog!).FirstOrDefaultAsync();
            return post;
        }
    }
}
