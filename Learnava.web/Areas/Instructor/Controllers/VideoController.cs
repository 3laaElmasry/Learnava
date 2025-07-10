using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Learnava.web.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Authorize(Roles = $"{SD.Role_Admin},{SD.Role_Instructor}")]
    public class VideoController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IVideoService _videoService;

        public VideoController(ICourseService courseService, IVideoService videoService)
        {
            _courseService = courseService;
            _videoService = videoService;
        }

        public async Task<IActionResult> Index(int courseId)
        {
            var course = await _courseService.GetCourseByIdAsync(courseId);

            if(course is null)
            {
                return NotFound();
            }
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!;

            if(userId != course.InstructorId && !User.IsInRole(SD.Role_Admin))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            ViewData["courseName"] = course.Title;
            var videos = await _videoService.GetVideosAsync(v => v.CourseId == course.Id);
            return View(videos);
        }


    }
}
