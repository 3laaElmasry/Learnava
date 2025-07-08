using Learnava.DataAccess.Data.Entities;

namespace Learnava.web.VMModels
{
    public class CourseVM
    {
        public Course? course;
        public string InstructorName { get; set; } = string.Empty;
    }
}
