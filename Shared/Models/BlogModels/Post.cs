using System.ComponentModel.DataAnnotations;

namespace SharedClasses.Models.BlogModels
{
    /// <summary>
    /// A single blog entry
    /// </summary>
    public class Post
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

        /// <summary>
        /// Date of blog post
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// Id of blog this post belongs to
        /// </summary>
        public Guid? BlogId { get; set; }

        /// <summary>
        /// Blog this post belongs to
        /// </summary>
        public Blog? Blog { get; set; }

        /// <summary>
        /// Route to get to this post
        /// </summary>
        public string? Route {  get; set; }
    }
}
