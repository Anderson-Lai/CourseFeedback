using CourseFeedback.Areas.Identity.Data;
using CourseFeedback.Data;
using CourseFeedback.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseFeedback.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
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

        [HttpPost]
        public async Task<IActionResult> Reply(string id, Replies reply)
        {
            var guid = new Guid(id);
            var user = await userManager.GetUserAsync(User);

            var newReply = new Replies
            {
                Text = reply.Text,
                TimeCreated = DateTime.Now,
                CommentId = guid,
                UserId = user.Id,
            };

            await dbContext.Replies.AddAsync(newReply);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Comments", new {id = id});
        }
    }
}
