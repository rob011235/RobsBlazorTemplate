using Microsoft.EntityFrameworkCore;
using Server.Data;
using SharedClasses.Models.BlogModels;

namespace Server.Services.DALs
{
    public class ManageBlogPageDAL(ApplicationDbContext context)
    {
        public async Task<Blog?> GetBlogByRouteAsync(string route)
        {
            return await context.Blogs.Where(b => b.Route == route).Include(b=>b.Posts).FirstOrDefaultAsync();
        }

        public async Task<Post?> GetPostAsync(Guid id)
        {
            return await context.Posts.Where(p=>p.Id == id).Include(p=>p.Blog).FirstOrDefaultAsync();
        }

        public async Task AddPostAsync(Post post)
        {
            context.Posts.Add(post);
            await context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            context.Posts.Update(post);
            await context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(Guid id)
        {
            Post? post = await context.Posts.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (post != null)
            {
                context.Posts.Remove(post);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateBlogAsync(Blog blog)
        {
            context.Blogs.Update(blog);
            await context.SaveChangesAsync();
        }
    }
}
