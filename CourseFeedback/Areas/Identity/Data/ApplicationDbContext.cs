using CourseFeedback.Areas.Identity.Data;
using CourseFeedback.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CourseFeedback.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Courses> Courses { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Replies> Replies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Courses>().HasData(
                new Courses
                {
                    CourseCode = "ICS3U1",
                    CreditNumber = 1,
                    CourseName = "Introduction to Computer Science",
                    Description = "This course introduces students to computer science. Students will design software independently and as part of a team, using industry-standard programming tools and applying the software development life-cycle model. They will also write and use subprograms within computer programs. Students will develop creative solutions for various types of problems as their understanding of the computing environment grows. They will also explore environmental and ergonomic issues, emerging research in computer science, and global career trends in computer-related fields.\r\n\r\n\r\nStudents will incorporate Catholic social teachings as they become critical and innovative problem-solvers who question the use of human and physical resources as well as understand the implications of computers and related innovations. An emphasis on problem solving models helps students create solutions that recognize our God-given responsibility to respect the dignity and value of the individual, the protection of the environment and ethical and moral use of the world’s resources.",
                    ELearning = false,
                    GuidanceMessage = "you should take this course",
                    Openness = "University",
                    Prerequisites = new List<string>()
                    {
                        "AMU105"
                    },
                    Corequisites = new List<string>()
                    {
                        "SNC3b",
                        "ENG2D5f",
                    }
                }
            );

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
