using Microsoft.AspNetCore.Mvc;

namespace SecretSanta_Core.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
