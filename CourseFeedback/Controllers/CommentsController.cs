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
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(string id, Replies reply)
        {
            var guid = new Guid(id);
            var user = await userManager.GetUserAsync(User);

            var newReply = new Replies
            {
                Text = reply.Text ?? string.Empty,
                TimeCreated = DateTime.Now,
                CommentId = guid,
                UserId = user.Id,
            };

            var associatedComment = await dbContext.Comments.FindAsync(guid);
            associatedComment.NumberOfReplies++;

            await dbContext.Replies.AddAsync(newReply);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Comments", new {id = id});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            var guid = new Guid(id);

            var result = await dbContext.Replies.FindAsync(guid);

            if(result is null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(result);
            
        }

        [HttpGet]
        public async Task<IActionResult> HandleEditError(string id)
        {
            var guid = new Guid(id);

            var result = await dbContext.Comments.FindAsync(guid);

            return RedirectToAction("Index", "Comments", new {id = result.Id});
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, Replies reply)
        {
            var guid = new Guid(id);

            var result = await dbContext.Replies.FindAsync(guid);

            result.Text = reply.Text ?? string.Empty;
            result.TimeEdited = DateTime.Now;
            result.Edited = true;

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Comments", new { id = result.CommentId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var guid = new Guid(id);

            var result = await dbContext.Replies.AsNoTracking().
                FirstOrDefaultAsync(c => c.Id == guid);

            dbContext.Replies.Remove(result);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Comments", new {id = result.CommentId});

        }
    }
}
