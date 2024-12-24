using Server.Data;
using SharedClasses.Models.BlogModels;

namespace Server.Services.DataServices
{
    public class BlogPageDataService(ApplicationDbContext context)
    {
        public List<Blog> GetAllBlogs()
        {
            return context.Blogs.ToList();
        }

        public Blog? GetBlog(Guid id)
        {
            return context.Blogs.FirstOrDefault() as Blog;
        }
    }
}
