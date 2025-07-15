using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess;
using Learnava.DataAccess.Data.Entities;
using Learnava.web.VMModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

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

            if (course is null)
            {
                return NotFound();
            }
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!;

            if (userId != course.InstructorId && !User.IsInRole(SD.Role_Admin))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            ViewData["courseName"] = course.Title;
            var videos = await _videoService.GetVideosAsync(v => v.CourseId == course.Id);
            return View(videos);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id, int? courseId)
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!;

            var courses = await _courseService.GetCoursesAsync(v => v.InstructorId == userId || User.IsInRole(SD.Role_Admin));
            var courseList = courses.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            });

            if (id == null)
            {

                var viewModel = new VideoUpsertViewModel
                {
                    Video = new Video(),


                    CourseList = courseList
                };

                return View(viewModel);
            }

            var video = await _videoService.GetVideoByIdAsync(id.Value);
            if (video == null)
                return NotFound();

            var editViewModel = new VideoUpsertViewModel
            {
                Video = video,
                CourseList = courseList
            };

            return View(editViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(VideoUpsertViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!;

                var courses = await _courseService.GetCoursesAsync(v => v.InstructorId == userId || User.IsInRole(SD.Role_Admin));

                vm.CourseList = courses.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                });

                return View(vm);
            }

            if (vm.Video.Id == 0)
            {
                vm.Video.UploadedAt = DateTime.UtcNow;
                await _videoService.AddVideoAsync(vm.Video);
                TempData["success"] = "Video created successfully.";
            }
            else
            {
                await _videoService.UpdateVideoAsync(vm.Video.Id, vm.Video);
                TempData["success"] = "Video updated successfully.";
            }

            return RedirectToAction("Index", new { courseId = vm.Video.CourseId });
        }

        [HttpDelete]

        public async Task<IActionResult> Delete(int id)
        {

            bool isDeleted = await _videoService.DeleteVideoAsync(id);
            if (!isDeleted)
            {
                return Json(new { success = false, message = "Video not found." });
            }

            return Json(new { success = true, message = "Video deleted successfully." });
        }

    }
}