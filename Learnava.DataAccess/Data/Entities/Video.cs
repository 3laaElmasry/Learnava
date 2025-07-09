

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Learnava.DataAccess.Data.Entities
{
    public class Video
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Url { get; set; } = string.Empty;         
        public DateTime UploadedAt { get; set; }


        public int Position { get; set; } = 1;

        [Required]
        public int CourseId { get; set; }


        [ValidateNever]
        public Course? Course { get; set; }
    }

}
