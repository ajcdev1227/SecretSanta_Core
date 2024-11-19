using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SecretSanta_Core.BusinessLogic;
using SecretSanta_Core.Data;
using SecretSanta_Core.Models;

namespace SecretSanta_Core.Controllers
{
    public class LearnMoreController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;
        private const int PageSize = 30;

        public LearnMoreController(ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }
        
        public IActionResult Contact()
        {
            ViewBag.EmailTo = _appSettings.EmailTo;
            ViewBag.VolunteerCenter = _appSettings.VolunteerCenter;
            return View();
        }

        public IActionResult Sponsors(int? id)
        {
            int pageNumber;
            if (!id.HasValue)
            {
                pageNumber = 1;
            }
            else
            {
                pageNumber = id.Value;

            }

            var sps = _context.TblSponsors.ToList().Where(s => s.IsActive).OrderBy(s => s.SponsorName);
            var sponsors = sps.Skip((pageNumber - 1) * PageSize).Take(PageSize);

            ViewBag.Count = sps.Count();
            ViewBag.PageSize = PageSize;
            ViewBag.PageNumber = pageNumber;

            return View(sponsors);
        }

        public ActionResult DropOffLocations(int? id)
        {
            int pageNumber;
            if (!id.HasValue)
            {
                pageNumber = 1;
            }
            else
            {
                pageNumber = id.Value;

            }

            List<TblSponsor> sps = new List<TblSponsor>();
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("ZipCode")))
            {
                var zipCode = HttpContext.Session.GetString("ZipCode");
                sps = _context.TblSponsors
                    .Where(s => s.IsActive && s.SponsorZip == zipCode && s.OpenToPublic)
                    .OrderBy(s => s.SponsorName)
                    .ToList();
            }
            else
            {
                sps = _context.TblSponsors
                    .Where(s => s.IsActive && s.OpenToPublic)
                    .OrderBy(s => s.SponsorName).ToList();
            }

            var sponsors = sps.Skip((pageNumber - 1) * PageSize).Take(PageSize);

            var model = new List<SponsorModel>();

            foreach (var s in sponsors)
            {
                var sponsor = new SponsorModel();
                sponsor.SponsorName = s.SponsorName;
                sponsor.Location = $"{s.SponsorStreet} {s.SponsorCity}, {s.SponsorState} {s.SponsorZip}";
                var googleUrl =
                    BusinessMethods.GetGoogleUrl(s.SponsorStreet, s.SponsorCity, s.SponsorState, s.SponsorZip);
                if (!string.IsNullOrEmpty(googleUrl))
                {
                    sponsor.GoogleUrl =
                        googleUrl;
                }

                model.Add(sponsor);
            }

            ViewBag.Count = sps.Count();
            ViewBag.PageSize = PageSize;
            ViewBag.PageNumber = pageNumber;
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("ZipCode")))
            {
                ViewBag.ZipCode = HttpContext.Session.GetString("ZipCode");
            }
            else
            {
                ViewBag.ZipCode = String.Empty;
            }


            return View(model);
        }

        public ActionResult DropOffZipCode(string zipCode)
        {
            if (!String.IsNullOrEmpty(zipCode))
            {
                HttpContext.Session.SetString("ZipCode", zipCode);
            }
           
            return RedirectToAction("DropOffLocations", new { id = 1 });
        }

        public ActionResult Agencies()
        {
            var agencies = _context.TblAgencies.Where(a => a.IsActive).OrderBy(a => a.AgencyName);
            //var model = agencies.Select(a => new AgencyModel
            //{
            //    AgencyName = a.AgencyName,
            //    AgencyStreet = a.AgencyStreet,
            //    AgencyCity = a.AgencyCity,
            //    AgencyState = a.AgencyState,
            //    AgencyZip = a.AgencyZip
            //});
            return View(agencies);
        }


    }
}
