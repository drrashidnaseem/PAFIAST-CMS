using Microsoft.AspNetCore.Mvc;
using AuthSystem.Data;
using AuthSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AuthSystem.Controllers
{
    public class MCQController : Controller

    {
        private readonly AuthDbContext _test;
        public MCQController(AuthDbContext test)
        {

            _test = test;
        }
        [Authorize]

        public ActionResult Index()
        {
            IEnumerable<MCQ> getQuestions = _test.MCQs.Include(q => q.Subject);
            return View(getQuestions);
        }
        [Authorize]
        public IActionResult Create()
        {
            int? selectedsubjectId = HttpContext.Session.GetInt32("SelectedSubjectId");
            if (selectedsubjectId == null )
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MCQ obj)
        {
            int? selectedsubjectId = HttpContext.Session.GetInt32("SelectedSubjectId");
          
            obj.SubjectId = selectedsubjectId.Value;
            _test.MCQs.Add(obj);
            _test.SaveChanges();
            HttpContext.Session.Clear();
            return RedirectToAction("Index");

        }
    }
   
}
