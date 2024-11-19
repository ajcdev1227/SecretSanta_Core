using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecretSanta_Core.BusinessLogic;
using SecretSanta_Core.Data;
using SecretSanta_Core.Models;

namespace SecretSanta_Core.Controllers
{
    public class StoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            var stories = _context.TblStories.ToList().Select(s => new StoryModel
            {
                StoryId = s.StoryId,
                Title = s.Title,
                StoryContent = s.StoryContent,
                CreatedDateTime = s.CreatedDateTime,
                IsActive = s.IsActive
            });
            return View(stories);
        }

        public ActionResult Add()
        {
            StoryModel model = new StoryModel
            {
                IsActive = true
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(StoryModel model)
        {
            if (ModelState.IsValid)
            {

                var story = new TblStory
                {
                    Title = model.Title,
                    StoryContent = model.StoryContent,
                    CreatedDateTime = BusinessMethods.GetLocalDateTime(DateTime.UtcNow),
                    IsActive = model.IsActive
                };
                _context.TblStories.Add(story);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Story content is required");
            return View(model);

        }

        public ActionResult Edit(int id)
        {
            var story = _context.TblStories.FirstOrDefault(s => s.StoryId == id);
            var model = new StoryModel
            {
                StoryId = story.StoryId,
                Title = story.Title,
                StoryContent = story.StoryContent,
                IsActive = story.IsActive
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(StoryModel model)
        {
            if (ModelState.IsValid)
            {
                var story = _context.TblStories.FirstOrDefault(s => s.StoryId == model.StoryId);
                story.Title = model.Title;
                story.StoryContent = model.StoryContent;
                story.IsActive = model.IsActive;
                _context.Update(story);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Story content is required");
            return View(model);

        }


    }
}
