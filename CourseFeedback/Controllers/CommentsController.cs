using CourseFeedback.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseFeedback.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public CommentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var guid = new Guid(id);

            var result = await dbContext.Comments.Include(c => c.Replies)
                .FirstOrDefaultAsync(c => c.Id == guid);

            return View(result);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Reply()
        {
            return View();
        }
    }
}
