using SharedClasses.Interfaces;
using SharedClasses.Models.BlogModels;
using System.Net.Http.Json;

namespace WebClient.Services.DataServices
{
    public class ClientBlogPageDataService(HttpClient http) : IBlogPageDataService

    {
        public async Task<Blog?> GetBlogFromRouteAsync(string route)
        {
            return await http.GetFromJsonAsync<Blog?>($"api/blog/{route}");
        }
    }
}
