using System;

namespace URLShortner.Service.Models
{
    public class UrlDto
    {
        public int UrlId { get; set; }

        public string LongUrl { get; set; }

        public string ShortUrl { get; set; }

        public int Hits { get; set; }

        public DateTime GeneratedDate { get; set; }
    }
}
