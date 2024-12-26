using SharedClasses.Models.BlogModels;

namespace SharedClasses.Interfaces
{
    public interface IPostPageDAL
    {
        Task<Post?> GetPostFromRouteAsync(string route);
    }
}