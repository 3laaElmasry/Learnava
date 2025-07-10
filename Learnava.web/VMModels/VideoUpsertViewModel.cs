using Learnava.DataAccess.Data.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Learnava.web.VMModels
{
    public class VideoUpsertViewModel
    {
        public Video Video { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CourseList { get; set; }
    }

}
