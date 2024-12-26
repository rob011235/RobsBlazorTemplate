using Microsoft.EntityFrameworkCore;
using Server.Data;
using SharedClasses.Models.BlogModels;

namespace Server.Services.DALs
{
    public class ManageBlogsPageDAL(ApplicationDbContext context)
    {
        public async Task<List<Blog>> GetBlogsAsync()
        {
            return await context.Blogs.ToListAsync();
        }

        public async Task<Blog?> GetBlogAsync(Guid id)
        {
            return await context.Blogs.Where(b=>b.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Blog?> GetBlogFromTitleAsync(string title)
        {
            return await context.Blogs.Where(b => b.Title == title).FirstOrDefaultAsync();
        }

        public async Task<Blog> AddAsync(Blog blog)
        {
            blog.Id = Guid.NewGuid();
            context.Blogs.Add(blog);
            await context.SaveChangesAsync();
            return blog;
        }

        public async Task UpdateAsync(Blog blog)
        {
            context.Blogs.Update(blog);
            await context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var blog = context.Blogs.Where(b => b.Id == id).FirstOrDefault();
            if (blog != null) 
            {
                context.Blogs.Remove(blog);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
