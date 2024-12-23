using System.ComponentModel.DataAnnotations;

namespace SharedClasses.Models.Blog
{
    /// <summary>
    /// Class that represents a blog that has a collection of posts.
    /// </summary>
    public class Blog
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        Guid Id { get; set; }
        /// <summary>
        /// Title for the blog. Keep to one line.
        /// </summary>
        [MaxLength(80)]
        public string? Title { get; set; }
        /// <summary>
        /// Description of the blog.
        /// </summary>
        public string? Description { get; set; }
    }
}
