using System.ComponentModel.DataAnnotations;

namespace CourseFeedback.Models
{
    public class Courses
    {
        [Key]
        public string CourseCode { get; set; } = string.Empty;

        public string CourseName { get; set; } = string.Empty;

        public int CreditNumber { get; set; }

        public string Openness { get; set; } = string.Empty;
        // university, college, open, etc.
        public List<string> Prerequisites { get; set; } = new List<string>();

        public List<string> Corequisites { get; set; } = new List<string>();

        public string Description { get; set; } = string.Empty;

        public string GuidanceMessage { get; set; } = string.Empty;

        public bool ELearning { get; set; }

        public ICollection<Comments> Comments { get; set; }
    }
}
