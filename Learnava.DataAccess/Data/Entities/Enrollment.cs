

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Learnava.DataAccess.Data.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }

        public int CourseId { get; set; }

        public int StudentId { get; set; }


        [ValidateNever]
        public Student? Student { get; set; }

        [ValidateNever]
        public Course? Course { get; set; }
    }
}
