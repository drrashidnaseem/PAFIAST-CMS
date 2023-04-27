using AuthSystem.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace AuthSystem.Controllers
{
    public class SubjectController : Controller
    {
        private readonly AuthDbContext _test;
        public SubjectController(AuthDbContext test)
        {

            _test = test;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var subjects = _test.Subjects.ToList();
            var viewModel = new Subject
            {
                Subjects = subjects
            };
            return View(viewModel);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Subjects()
        {
            var subjects = _test.Subjects.ToList();
            var viewModel = new Subject
            {
                Subjects = subjects
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Create(Subject model)
        {

            // Create a new Subject object from the model data
            Subject newSubject = new Subject { SubjectName = model.SubjectName };

            // Add the new subject to the database
            _test.Subjects.Add(newSubject);

            {
                _test.SaveChanges();
            }


            return RedirectToAction("Index");




        }
        public IActionResult SelectSubject(int subjectId)
        {
            HttpContext.Session.SetInt32("SelectedSubjectId", subjectId);

            return RedirectToAction("Index", "QuestionType");
        }
        public IActionResult DeleteSubject( int id) {
            var SubjectData = _test.Subjects.Find(id);
            var MCQ = _test.MCQs.Where(x => x.SubjectId == id);
            var FIB = _test.Blanks.Where(x => x.SubjectId == id);
            if (SubjectData == null) {

                return NotFound();
                    }
            _test.Subjects.Remove(SubjectData);
            _test.MCQs.RemoveRange(MCQ);
            _test.Blanks.RemoveRange(FIB);
            _test.SaveChanges();
            return RedirectToAction("Index");

        
        }

    }
}
