using CourseFeedback.Areas.Identity.Data;
using CourseFeedback.Data;
using CourseFeedback.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseFeedback.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var user = await dbContext.Users.Include(c => c.Comments)
                .FirstOrDefaultAsync(c => c.Id == id);

            return View(user);
        }

        [HttpGet]
        public IActionResult ToCourse(string id)
        {
            var course = new Courses
            {
                CourseCode = id,
            };

            return RedirectToAction("Index", "Courses", course);
        }
    }
}
