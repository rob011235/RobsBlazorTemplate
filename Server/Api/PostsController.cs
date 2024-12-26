using Microsoft.AspNetCore.Mvc;
using SharedClasses.Interfaces;
using SharedClasses.Models.BlogModels;

namespace Server.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController(IPostPageDAL dal) : ControllerBase
    {
        // GET api/Posts/my-post
        [HttpGet("{route}")]
        public async Task<Post?> GetPostFromRouteAsync(string route)
        {
            return await dal.GetPostFromRouteAsync(route);
        }
    }
}
