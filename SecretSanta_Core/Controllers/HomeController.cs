using Microsoft.AspNetCore.Mvc;
using SecretSanta_Core.Models;
using System.Diagnostics;
using Microsoft.Extensions.Options;
using NuGet.Protocol.Plugins;
using SecretSanta_Core.Data;

namespace SecretSanta_Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _context = context;
            _appSettings = appSettings.Value;
        }

        public IActionResult Index()
        {
            string homeText = String.Empty;
            var applicationSetting = _context.ApplicationSettings.FirstOrDefault(s => s.SettingsName == "Home Text");
            if (applicationSetting != null)
            {
                homeText = applicationSetting.SettingsValue;
            }
            ViewBag.HomeText = homeText;
            string homeImageUrl = String.Empty;
            var imageSetting = _context.ApplicationSettings.FirstOrDefault(s => s.SettingsName == "Home Image");
            if (imageSetting != null)
            {
                homeImageUrl = $"{_appSettings.StorageUriBase}gallery/" + imageSetting.SettingsValue;
            }
            ViewBag.HomeImageUrl = homeImageUrl;
            string sponsorImageUrl = String.Empty;
            var sponsorImageSetting = _context.ApplicationSettings.FirstOrDefault(s => s.SettingsName == "Sponsor Image");
            if (sponsorImageSetting != null)
            {
                sponsorImageUrl = $"{_appSettings.StorageUriBase}gallery/" + sponsorImageSetting.SettingsValue;
            }
            ViewBag.SponsorImageUrl = sponsorImageUrl;
            List<TblStory> stories = _context.TblStories.Where(s=>s.IsActive).ToList();
            var model = new HomeModel
            {
                Stories = stories
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}