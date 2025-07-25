﻿

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Learnava.DataAccess.Data.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public string InstructorId { get; set; } = string.Empty ;

        public string ImgUrl { get; set; } = string.Empty;

        [ValidateNever]
        public ApplicationUser? Instructor { get; set; }

        [ValidateNever]

        public IEnumerable<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

        [ValidateNever]
        public IEnumerable<Video> Videos { get; set; } = new List<Video>();
    }
}
