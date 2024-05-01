using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CourseFeedback.Areas.Identity.Data;

namespace CourseFeedback.Models
{
    public class Replies
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime TimeCreated { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string Text { get; set; } = string.Empty;

        [ForeignKey(nameof(Comments))]
        public Guid CommentId { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; } = string.Empty;

        public bool Edited { get; set; } = false;

        public DateTime TimeEdited { get; set; }

        public Comments Comments { get; set; }
    }
}
