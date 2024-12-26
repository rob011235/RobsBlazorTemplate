using System.ComponentModel.DataAnnotations;

namespace SharedClasses.Models.BlogModels
{
    /// <summary>
    /// Class that represents a blog that has a collection of posts.
    /// </summary>
    public class Blog
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Title for the blog. Keep to one line.
        /// </summary>
        [MaxLength(80)]
        public string? Title { get; set; }
        /// <summary>
        /// Description of the blog.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Navigation route you want for this blog (<domain>/<route>).
        /// </summary>
        public string? Route { get; set; }

        /// <summary>
        /// Posts associated with this blog
        /// </summary>
        public List<Post>? Posts { get; set; }

        /// <summary>
        /// Should blog be displayed
        /// </summary>
        public bool DisplayBlog { get; set; } = false;
    }
}
