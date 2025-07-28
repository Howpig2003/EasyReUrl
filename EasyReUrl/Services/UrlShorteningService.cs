using EasyReUrl.Data;
using EasyReUrl.Models.UrlManagement;
using Microsoft.EntityFrameworkCore;

namespace EasyReUrl.Services
{
    public class UrlShorteningService
    {
        private readonly AppDbContext _dbContext;
        private const int MaxAttempts = 10; // 每次嘗試生成短碼的最大次數

        public UrlShorteningService(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<string> GenerateShortUrl(string originalUrl)
        {
            for (int attempt = 0; attempt < MaxAttempts; attempt++)
            {
                string shortCode = GenerateRandomCode(6);
                var existingUrl = await _dbContext.ShortUrls
                    .FirstOrDefaultAsync(u => u.ShortenedUrl == shortCode);

                if (existingUrl == null)
                {
                    // 短碼不存在，直接使用
                    var shortUrl = new ShortUrl
                    {
                        OriginalUrl = originalUrl,
                        ShortenedUrl = shortCode,
                        CreatedAt = DateTime.UtcNow,
                        UsageCount = 0
                    };

                    _dbContext.ShortUrls.Add(shortUrl);
                    await _dbContext.SaveChangesAsync();
                    return shortCode;
                }
                else if (existingUrl.CreatedAt < DateTime.UtcNow.AddDays(-7))
                {
                    // 短碼存在但已超過 7 天，重用該短碼
                    existingUrl.OriginalUrl = originalUrl;
                    existingUrl.CreatedAt = DateTime.UtcNow;
                    existingUrl.UsageCount = 0;
                    await _dbContext.SaveChangesAsync();
                    return shortCode;
                }
                // 如果短碼存在且未過期，繼續嘗試生成新短碼
            }

            throw new InvalidOperationException("無法生成短網址，請稍後重試或聯繫管理員。");
        }

        public async Task<ShortUrl?> GetOriginalUrlAsync(string shortCode)
        {
            var shortUrl = await _dbContext.ShortUrls
                .FirstOrDefaultAsync(u => u.ShortenedUrl == shortCode);

            if (shortUrl != null)
            {
                shortUrl.UsageCount++;
                await _dbContext.SaveChangesAsync();
            }

            return shortUrl;
        }

        private string GenerateRandomCode(int length)
        {
            //"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz23456789";
            var random = new Random();
            var code = new char[length];
            for (int i = 0; i < length; i++)
            {
                code[i] = chars[random.Next(chars.Length)];
            }
            return new string(code);
        }
    }
}