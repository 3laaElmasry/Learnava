﻿

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Learnava.DataAccess.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;


        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

        [ValidateNever]
       public IEnumerable<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
