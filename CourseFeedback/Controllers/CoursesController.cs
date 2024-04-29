using CourseFeedback.Areas.Identity.Data;
using CourseFeedback.Data;
using CourseFeedback.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseFeedback.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Index(Courses course)
        {
            var result = await dbContext.Courses.Include(c => c.Comments)
                .FirstOrDefaultAsync(c => c.CourseCode == course.CourseCode);
                
            // Include tells path for EF core
            // firstordefault => finds first course whose id matches
            // first "(class)Course's coursecode which mathces
            // the function parameter's coursecode

            return View(result);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string id, Comments comment)
        {

            var user = await userManager.GetUserAsync(User);

            var temp = new Comments
            {
                Text = comment.Text,
                TimeCreated = DateTime.Now,
                CourseCode = id,
                UserId = user.Id,
            };

            await dbContext.Comments.AddAsync(temp);
            await dbContext.SaveChangesAsync();

            var course = new Courses
            {
                CourseCode = id
            };

            return RedirectToAction("Index", "Courses", course);
        }

        [HttpGet]
        [Authorize]
        // id is from Index.cshtml
        // asp-route-id is set to @Model.CourseCode where @model Courses
        public async Task<IActionResult> Edit(string id)
        {
            var guid = new Guid(id); 
            // typecast to Guid as Comments.Id is of type Guid and not string / int

            var comment = await dbContext.Comments.FindAsync(guid);
            
            return View(comment);
        }

        [HttpGet]
        // works bc asp-route-id is set to @Model.CourseCode
        // @model Comments
        public IActionResult HandleEditError(string id)
            // make sure parameter name is "id" when using asp-route-id
        {
            var course = new Courses
            {
                CourseCode = id,
            };

            return RedirectToAction("Index", "Courses", course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Comments comment)
        {
            var guId = new Guid(id);

            var sysComment = await dbContext.Comments.FindAsync(guId);

            sysComment.Text = comment.Text;
            sysComment.Edited = true;

            await dbContext.SaveChangesAsync();

            var course = new Courses
            {
                CourseCode = sysComment.CourseCode,
            };

            return RedirectToAction("Index", "Courses", course);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var guid = new Guid(id);

            var course = await dbContext.Comments.FindAsync(guid);

            return View(course);
        }

        [HttpPost]
        public IActionResult HandleDeleteError(string id)
        {
            var course = new Courses
            {
                CourseCode = id,
            };

            return RedirectToAction("Index", "Courses", course);
        }

        [HttpGet]
        public async Task<IActionResult> HandleDelete(string id)
        {
            var guid = new Guid(id);

            var comment = dbContext.Comments.AsNoTracking().FirstOrDefault(c => c.Id == guid);

            var course = new Courses
            {
                CourseCode = comment.CourseCode,
            };

            dbContext.Comments.Remove(comment);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Courses", course);
        }
    }
}
