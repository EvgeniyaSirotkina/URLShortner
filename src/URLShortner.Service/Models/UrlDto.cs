using System;

namespace URLShortner.Service.Models
{
    /// <summary>
    /// Data transfer object for Url model.
    /// </summary>
    public class UrlDto
    {
        /// <summary>
        /// Unique identifier for Url object.
        /// </summary>
        public int UrlId { get; set; }

        /// <summary>
        /// Provided Url.
        /// </summary>
        public string LongUrl { get; set; }

        /// <summary>
        /// Generated short Url.
        /// </summary>
        public string ShortUrl { get; set; }

        /// <summary>
        /// Number of clicks on a short link.
        /// </summary>
        public int Hits { get; set; }

        /// <summary>
        /// Short Url generation date.
        /// </summary>
        public DateTime GeneratedDate { get; set; }
    }
}
