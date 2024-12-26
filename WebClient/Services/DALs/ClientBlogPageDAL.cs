using SharedClasses.Interfaces;
using SharedClasses.Models.BlogModels;
using System.Net.Http.Json;

namespace WebClient.Services.DALs
{
    public class ClientBlogPageDAL(HttpClient http) : IBlogPageDAL

    {
        public async Task<Blog?> GetBlogFromRouteAsync(string route)
        {
            return await http.GetFromJsonAsync<Blog?>($"api/blog/{route}");
        }
    }
}
