namespace SharedClasses.Models
{
    /// <summary>
    /// A dynamic nav link
    /// </summary>
    public class NavMenuItem
    {
        /// <summary>
        /// Title that the user will see
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Route or url to go to
        /// </summary>
        public string? Url { get; set; }
    }
}
