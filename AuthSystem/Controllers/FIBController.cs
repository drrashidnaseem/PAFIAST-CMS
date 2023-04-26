using Microsoft.AspNetCore.Mvc;

namespace AuthSystem.Controllers
{
    public class FIBController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
