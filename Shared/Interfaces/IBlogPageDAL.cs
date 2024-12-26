using SharedClasses.Models.BlogModels;

namespace SharedClasses.Interfaces
{
    public interface IBlogPageDAL
    {
        Task<Blog?> GetBlogFromRouteAsync(string route);
    }
}