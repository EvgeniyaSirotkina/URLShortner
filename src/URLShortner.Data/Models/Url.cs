using System;
using System.ComponentModel.DataAnnotations;

namespace URLShortner.Data.Models
{
    public class Url
    {
        [Key]
        public int UrlId { get; set; }

        [Required]
        public string LongUrl { get; set; }

        [Required]
        public string ShortUrl { get; set; }

        [Required]
        public int Hits { get; set; }

        [Required]
        public DateTime GeneratedDate { get; set; }
    }
}
