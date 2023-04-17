using Microsoft.AspNetCore.Mvc;

namespace AuthSystem.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
