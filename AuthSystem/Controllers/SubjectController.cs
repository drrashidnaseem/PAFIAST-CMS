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
        public IActionResult Index()
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


            // Redirect to the original view or another appropriate view
            return RedirectToAction("Index");



            // If model state is not valid, return to the same view with validation errors

        }
        public IActionResult SelectSubject(int subjectId)
        {
            // Store the selected subject in session
            HttpContext.Session.SetInt32("SelectedSubjectId", subjectId);

            // Redirect to the next screen
            return RedirectToAction("Index", "QuestionType");
        }
    }
}
