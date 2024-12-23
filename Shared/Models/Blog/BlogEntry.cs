using System.ComponentModel.DataAnnotations;

namespace SharedClasses.Models.Blog
{
    /// <summary>
    /// A single blog entry
    /// </summary>
    public class BlogEntry
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Title for the blog entry. Max length 80.
        /// </summary>
        [MaxLength(80)]
        public string? Title { get; set; }
        /// <summary>
        /// Text for the blog entry. This will be rendered as html.
        /// </summary>
        public string? Text { get; set; }
    }
}
