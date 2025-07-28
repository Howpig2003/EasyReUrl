using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using EasyReUrl.Models.UrlManagement;

namespace EasyReUrl.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ShortUrl> ShortUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 確保 ShortenedUrl 欄位唯一
            modelBuilder.Entity<ShortUrl>()
                .HasIndex(u => u.ShortenedUrl)
                .IsUnique();
        }
    }
}