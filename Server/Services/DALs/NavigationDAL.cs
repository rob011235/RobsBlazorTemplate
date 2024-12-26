using Microsoft.AspNetCore.Components.Routing;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using SharedClasses.Models;
using SharedClasses.Models.BlogModels;

namespace Server.Services.DALs
{
    public class NavigationDAL(ApplicationDbContext context)
    {
        public async Task<List<NavMenuItem>> GetNavMenuItemsAsync()
        {
            List<NavMenuItem> links = new List<NavMenuItem>();
            List<Blog> blogs = await context.Blogs.Where(b => b.DisplayBlog).ToListAsync();
            foreach (Blog blog in blogs)
            {
                links.Add(new NavMenuItem
                {
                    Title = blog.Title,
                    Url = $"blog/{blog.Route}"
                });
            }
            return links;
        }
    }
}
