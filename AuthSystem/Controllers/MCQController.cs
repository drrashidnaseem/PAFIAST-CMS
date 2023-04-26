using System.Web;
using Microsoft.AspNetCore.Mvc;
using AuthSystem.Data;
using AuthSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.CodeAnalysis.Differencing;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
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
        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<MCQ> getQuestions = _test.MCQs.Include(q => q.Subject);
            return View(getQuestions);
        }
        public ActionResult MCQs()
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
        [Authorize]

        public IActionResult Edit() {

            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? Id) { 
        if (Id == null)
            {

                return NotFound();
            }
            var EditMCQData = _test.MCQs.Find(Id);
            if (EditMCQData == null) {

                return NotFound();
            }
            return View(EditMCQData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MCQ obj) {

            if (ModelState.IsValid) {

                _test.MCQs.Update(obj);
                _test.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        
        }
        public IActionResult Delete(int? Id) {
            var mcqData = _test.MCQs.Find(Id);
            if (mcqData == null) {
                return NotFound();
            }
            _test.MCQs.Remove(mcqData);
            _test.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult View(int? Id) {

            if (Id == null) { 
            

                return NotFound();
            
            }
            var questionData = _test.MCQs.Find(Id);

            return View(questionData);
        
        }
        public IActionResult UploadFile() {

            return View();
        }
        [HttpPost]
        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("file", "Please select a file to upload.");
                return View();
            }

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("file", "Invalid file format. Please upload an Excel file with .xlsx extension.");
                return View();
            }

            var questions = new List<MCQ>();
            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        ModelState.AddModelError("file", "Excel file is empty or has no worksheets.");
                        return View();
                    }

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        var question = new MCQ
                        {
                            Content = worksheet.Cells[row, 1].Value?.ToString(),
                            Answer = worksheet.Cells[row, 2].Value?.ToString(),

                            Option1 = worksheet.Cells[row, 3].Value?.ToString(),
                            Option2 = worksheet.Cells[row, 4].Value?.ToString(),
                            Option3 = worksheet.Cells[row, 5].Value?.ToString(),
                            Difficulty = worksheet.Cells[row, 6].Value?.ToString(),
                            SubjectId = HttpContext.Session.GetInt32("SelectedSubjectId").Value


                    };
                        Console.WriteLine($"Row {row}:");
                        Console.WriteLine($"Content: {question.Content}");
                        Console.WriteLine($"Answer: {question.Answer}");
                        Console.WriteLine($"Option1: {question.Option1}");
                        Console.WriteLine($"Option2: {question.Option2}");
                        Console.WriteLine($"Option3: {question.Option3}");
                        Console.WriteLine($"Difficulty: {question.Difficulty}");
                        Console.WriteLine($"SubjectId: {question.SubjectId}");
                        if (string.IsNullOrEmpty(question.Answer))
                        {
                            ModelState.AddModelError("Answer", $"The answer for row {row} is required.");
                            return View();
                        }

                        questions.Add(question);
                    }

                    if (!ModelState.IsValid)
                    {
                        return View();
                    }
                }
            }

            // Save the questions to the database
            _test.MCQs.AddRange(questions);
            _test.SaveChanges();
            return RedirectToAction("Index");
        }



    }

}
