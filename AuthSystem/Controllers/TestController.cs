using AuthSystem.Data;
using AuthSystem.Migrations;
using AuthSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.JavaScript;

namespace AuthSystem.Controllers
{
    public class TestController : Controller
    {
        private readonly AuthDbContext _test;
        public TestController(AuthDbContext test) {

            _test = test;
        
        }
        public IActionResult Test()
        {
            var viewModel = new Test
            {
                TestList = _test.Tests.ToList(),
                Subjects = _test.Subjects.ToList()
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string testName, int[] selectedSubjectIds, Dictionary<int, int> percentages)
        {
            if (ModelState.IsValid)
            {
                var test = new Test { TestName = testName };
                _test.Tests.Add(test);
                await _test.SaveChangesAsync();

                foreach (var subjectId in selectedSubjectIds)
                {
                    var testDetails = new TestDetail
                    {
                        TestId = test.TestId,
                        SubjectId = subjectId,
                        Percentage = percentages[subjectId]
                    };

                    _test.TestsDetail.Add(testDetails);
                }

                await _test.SaveChangesAsync();

                return RedirectToAction("Test");
            }

            return RedirectToAction("Test");
        }

    }
}