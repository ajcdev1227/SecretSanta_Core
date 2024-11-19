using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SecretSanta_Core.Data;
using SecretSanta_Core.Models;
using SecretSanta_Core.Services;

namespace SecretSanta_Core.Controllers
{
	[Authorize(Roles = "Admin")]
	public class ApplicationSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly StorageService _storageService;
        private readonly AppSettings _appSettings;

        public ApplicationSettingsController(ApplicationDbContext context, StorageService storageService, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _storageService = storageService;
            _appSettings = appSettings.Value;
        }

		public IActionResult Index()
        {
            var applicationSettings = _context.ApplicationSettings;
            var adminSettings = applicationSettings.Where(s => s.Category == "Admin").ToList();
            var heartCentralSetting = applicationSettings.FirstOrDefault(s => s.Category == "HeartCentral");
            var homeImageSetting = applicationSettings.Where(s => s.Category == "Home Image").ToList();
            var model = new ApplicationSettingsModel();
            model.AdminSettings = adminSettings.Select(s => new SettingModel
            {
                SettingsID = s.SettingsId,
                SettingsName = s.SettingsName,
                SettingsValue = s.SettingsValue,
                Category = s.Category
            }).ToList();
            var dates = JsonConvert.DeserializeObject<TheseDates>(heartCentralSetting.SettingsValue);
            model.HeartCentralDates = new Dates { TheseDates = dates, Category = "HeartCentral" };

            model.AdminEmails = _context.TblAdminEmails.ToList();

            model.HomeImageModel = homeImageSetting.Select(s => new ImageSettingModel
            {
                SettingsID = s.SettingsId,
                SettingsName = s.SettingsName,
                ImageUri = $"{_appSettings.StorageUriBase}gallery/" + s.SettingsValue

            }).ToList();

            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(SettingModel model)
        {
            var setting = new ApplicationSetting
            {
                SettingsName = model.SettingsName,
                SettingsValue = model.SettingsValue,
                Category = "Admin"
            };

            _context.ApplicationSettings.Add(setting);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Edit(int id)
        {
            var existingSettings = _context.ApplicationSettings.FirstOrDefault(s => s.SettingsId == id);
            var model = new SettingModel
            {
                SettingsID = existingSettings.SettingsId,
                SettingsName = existingSettings.SettingsName,
                SettingsValue = existingSettings.SettingsValue
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(SettingModel model)
        {

            var existingSettings = _context.ApplicationSettings.FirstOrDefault(s => s.SettingsId == model.SettingsID);
            existingSettings.SettingsName = model.SettingsName;
            existingSettings.SettingsValue = model.SettingsValue;

            _context.ApplicationSettings.Update(existingSettings);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var setting = _context.ApplicationSettings.FirstOrDefault(s => s.SettingsId == id);
            _context.ApplicationSettings.Remove(setting);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult EditDates(string id)
        {
            var setting = _context.ApplicationSettings.FirstOrDefault(s => s.Category == id);
            var theseDates = JsonConvert.DeserializeObject<TheseDates>(setting.SettingsValue);
            var model = new Dates
            {
                TheseDates = theseDates,
                Category = id
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditDates(Dates model)
        {
            var setting = _context.ApplicationSettings.FirstOrDefault(s => s.Category == model.Category);
            var dates = JsonConvert.SerializeObject(model.TheseDates);

            setting.SettingsValue = dates;

            _context.ApplicationSettings.Update(setting);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult EditAdminEmail(int id)
        {
            var email = _context.TblAdminEmails.FirstOrDefault(l => l.EmailId == id);
            var model = new AdminEmailModel
            {
                EmailId = id,
                EmailName = email.EmailName,
                EmailContent = email.EmailContent
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditAdminEmail(AdminEmailModel model)
        {
            var email = _context.TblAdminEmails.FirstOrDefault(l => l.EmailId == model.EmailId);
            email.EmailContent = model.EmailContent;
            _context.TblAdminEmails.Update(email);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Image(int id)
        {
            HttpContext.Session.SetInt32("ImageSettingId", id);
            var blobContainerName = "gallery";
            var model = _storageService.GetBlobs(blobContainerName).OrderByDescending(i => i.LastModifiedDate).ToList();
            return View("Gallery", model);
        }

        public JsonResult SaveImageFiles(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                _storageService.AddImageToAzureStorage(file, "gallery", 900, 900);
            }

            return Json(new { Message = string.Empty });
        }

        public JsonResult GetImages()
        {
            var blobs = _storageService.GetBlobs("gallery").OrderByDescending(i => i.LastModifiedDate).ToList();
            var model = blobs.Select(b => new
            {
                blobImageName = b.BlobImageName,
                blobImageUri = b.BlobImageUri,
                lastModifiedDate = b.LastModifiedDate
            });
            return Json(new { model });
        }

        public JsonResult SetImage(string name)
        {
            var settingId = HttpContext.Session.GetInt32("ImageSettingId");
            var applicationSetting = _context.ApplicationSettings.FirstOrDefault(s => s.SettingsId == settingId);
            applicationSetting.SettingsValue = name;
            _context.Update(applicationSetting);
            _context.SaveChanges();

            return Json(new { Message = string.Empty });
        }
    }
}
