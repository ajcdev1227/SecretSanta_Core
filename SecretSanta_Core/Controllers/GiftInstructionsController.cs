using Microsoft.AspNetCore.Mvc;
using SecretSanta_Core.BusinessLogic;
using SecretSanta_Core.Data;
using SecretSanta_Core.Models;
using System.Transactions;
using SecretSanta_Core.Services;
using Newtonsoft.Json;

namespace SecretSanta_Core.Controllers
{
    public class GiftInstructionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExceptionUtilityService _logger;

        public GiftInstructionsController(ApplicationDbContext context, ExceptionUtilityService logger)
        {
            _context = context;
            _logger = logger;
        }

        public ActionResult PrintMoreLabels()
        {
            ViewBag.Now = BusinessMethods.GetLocalDateTime(DateTime.UtcNow).ToShortDateString();
            return View();
        }

        public JsonResult GetLabels(int numberGifts, int heartNumber)
        {
            var labels = new List<AdoptAHeartModel>();
            var labelCount = 0;

            var recipient = _context.TblRecipientParents.FirstOrDefault(r => r.RecipientNum == heartNumber);
            if (recipient == null)
            {
                var label = _context.TblRecipientChildren.FirstOrDefault(l => l.LabelNum == heartNumber);
                if (label != null)
                {
                    var recipientNumber = label.RecipientNum;
                    recipient = _context.TblRecipientParents.FirstOrDefault(r => r.RecipientNum == recipientNumber);
                    if (recipient == null)
                    {
                        return Json(new { labels = labels, labelCount });
                    }
                }
                else
                {
                    return Json(new { labels = labels, labelCount });
                }
            }


            var location = !String.IsNullOrEmpty(recipient.Location) ? recipient.Location : String.Empty;
            var agencyCode = _context.TblAgencies.FirstOrDefault(a => a.AgencyId == recipient.AgencyId).AgencyCode;
            var appId = _context.TblApps.FirstOrDefault(a => a.AppName == "AgencyWeb").AppId;
            labelCount = recipient.TblRecipientChildren.Count;

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            try
            {
                for (int i = 0; i < numberGifts - labelCount; i++)
                {

                    var recipientNumber = recipient.RecipientNum;
                    var child = new TblRecipientChild
                    {
                        RecipientNum = recipientNumber,
                        Primary = false
                    };
                    _context.TblRecipientChildren.Add(child);
                    _context.SaveChanges();

                    var labelNum = child.LabelNum;

                    var model = new AdoptAHeartModel
                    {
                        RecipientNumber = recipientNumber,
                        Name = recipient.Name,
                        Age = recipient.Age.Value,
                        AgencyCode = agencyCode,
                        LabelNum = labelNum,
                        Location = location
                    };

                    labels.Add(model);


                    var status = GenerateStatusLog(labelNum, (int)Enumerations.StatusTypes.HomeLabel, appId);

                    _context.TblStatusLogs.Add(status);
                }
                _context.SaveChanges();
                scope.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                scope.Dispose();
                throw;
            }


            return Json(new { labels = labels, labelCount });
        }

        private TblStatusLog GenerateStatusLog(int labelNum, int statusId, int appId)
        {
            var currentStatusText = String.Empty;
            var presentStatusText = BusinessMethods.GetStatusTypeText(statusId);

            var status = new TblStatusLog
            {
                LabelNum = labelNum,
                StatusId = statusId,
                DateEdited = BusinessMethods.GetLocalDateTime(DateTime.UtcNow),
                AppId = appId,
                ChangeInfo = $"Status Changed from {currentStatusText} to {presentStatusText}"
            };

            return status;
        }
    }
}
