using Microsoft.AspNetCore.Mvc;
using EasyReUrl.Models.UrlManagement;
using EasyReUrl.Services;

namespace EasyReUrl.Controllers
{
    [Route("/{*shortCode}")]
    public class UrlController : Controller
    {
        private readonly UrlShorteningService _urlService;

        public UrlController(UrlShorteningService urlService)
        {
            _urlService = urlService ?? throw new ArgumentNullException(nameof(urlService));
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/create")]
        public async Task<IActionResult> Create(string originalUrl)
        {
            if (!Uri.IsWellFormedUriString(originalUrl, UriKind.Absolute))
            {
                ModelState.AddModelError("OriginalUrl", "無效的網址格式");
                return View("Index");
            }

            var shortCode = await _urlService.GenerateShortUrl(originalUrl);
            var fullShortUrl = $"https://testdomain.com/{shortCode}";
            return View("Index", new { ShortUrl = fullShortUrl });
        }

        [HttpGet]
        public async Task<IActionResult> RedirectUrl(string shortCode)
        {
            var shortUrl = await _urlService.GetOriginalUrlAsync(shortCode);
            if (shortUrl == null)
            {
                return NotFound("短網址不存在");
            }

            return Redirect(shortUrl.OriginalUrl);
        }
    }
}