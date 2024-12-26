using Microsoft.AspNetCore.Mvc;
using Server.Services.DALs;
using SharedClasses.Interfaces;
using SharedClasses.Models.BlogModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController(IBlogPageDAL dal) : ControllerBase
    {
        // GET api/Blog/my-blog
        [HttpGet("{route}")]
        public async Task<Blog?> GetBlogFromRouteAsync(string route)
        {
            return await dal.GetBlogFromRouteAsync(route);
        }
    }
}
