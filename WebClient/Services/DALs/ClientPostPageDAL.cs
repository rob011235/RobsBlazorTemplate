using SharedClasses.Interfaces;
using SharedClasses.Models.BlogModels;
using System.Net.Http.Json;

namespace WebClient.Services.DALs
{
    public class ClientPostPageDAL(HttpClient http) : IPostPageDAL
    {
        public async Task<Post?> GetPostFromRouteAsync(string route)
        {
            return await http.GetFromJsonAsync<Post?>($"api/posts/{route}");
        }
    }
}
