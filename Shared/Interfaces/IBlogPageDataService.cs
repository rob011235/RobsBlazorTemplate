using SharedClasses.Models.BlogModels;

namespace SharedClasses.Interfaces
{
    public interface IBlogPageDataService
    {
        Task<Blog?> GetBlogFromRouteAsync(string route);
    }
}