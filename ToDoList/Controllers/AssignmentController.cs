using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class AssignmentController : Controller
    {
        ToDoAppDbContext context = new ToDoAppDbContext();
        public IActionResult Index(string name)
        {

            if(!string.IsNullOrEmpty(name))
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Secure = true;
                cookieOptions.Expires = DateTime.Now.AddDays(1);

                Response.Cookies.Append("saveName", name, cookieOptions);
            }

            ViewBag.Name = Request.Cookies["saveName"];

            var assis = context.Assignments.ToList();
            return View(assis);
        }
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult CreateNew()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateNew(Assignment assignment, IFormFile PdfFile)
        {
            if(PdfFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(PdfFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\pdfs", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    PdfFile.CopyTo(stream);
                }

                assignment.PdfFile = fileName;
            }

            context.Assignments.Add(assignment);
            context.SaveChanges();
            TempData["Added"] = "Task added successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int assignmentId) 
        {
            var items = context.Assignments.Find(assignmentId);
            if (items != null) 
                return View(items);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Edit(Assignment assignment)
        {
            context.Assignments.Update(assignment);
            context.SaveChanges();
            TempData["Updated"] = "Task Updated successfully";
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int assignmentId)
        {
            context.Assignments.Remove(new() { Id = assignmentId });
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
