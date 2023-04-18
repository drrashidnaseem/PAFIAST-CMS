using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthSystem.Controllers
{
    public class QuestionType : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
