

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Learnava.DataAccess.Data.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        public string ApplicationUserId {  get; set; } = string.Empty;


        [ValidateNever]
        public ApplicationUser? ApplicationUser { get; set; }

        [ValidateNever]
        IEnumerable<Course> Courses { get; set; } = new List<Course>();

    }
}
