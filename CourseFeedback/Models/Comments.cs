using CourseFeedback.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CourseFeedback.Models
{
    public class Comments
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime TimeCreated { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Text { get; set; } = string.Empty;

        [ForeignKey(nameof(Courses))]
        public string CourseCode { get; set; } = string.Empty;

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; } = string.Empty;

        public bool Edited { get; set; } = false;

        public int NumberOfReplies { get; set; } = 0;

        public DateTime TimeEdited { get; set; }

        public ICollection<Replies> Replies { get; set; }

        public Courses Courses { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
