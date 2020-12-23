using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using URLShortner.Data.Models;

namespace URLShortner.Data.EF
{
    public class UrlContext : DbContext
    {
        public UrlContext()
        {
        }

        public DbSet<Url> Urls { get; set; }
    }
}
