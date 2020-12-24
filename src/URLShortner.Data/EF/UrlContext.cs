using Microsoft.EntityFrameworkCore;
using URLShortner.Data.Models;

namespace URLShortner.Data.EF
{
    public class UrlContext : DbContext
    {
        public UrlContext(DbContextOptions<UrlContext> options)
            : base(options)
        {
        }

        public DbSet<Url> Urls { get; set; }
    }
}
