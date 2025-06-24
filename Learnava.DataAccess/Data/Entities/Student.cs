

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Learnava.DataAccess.Data.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public Guid ApplicationUserId { get; set; }


        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

        [ValidateNever]
       public IEnumerable<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
