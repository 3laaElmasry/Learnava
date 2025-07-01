using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess;
using Learnava.DataAccess.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;

namespace Learnava.web.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    [Route("[area]/[controller]/[action]")]
    [Authorize(Roles =$"{SD.Role_Admin},{SD.Role_Instructor}")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CourseController(ICourseService courseService, IWebHostEnvironment webHostEnvironment)
        {
            _courseService = courseService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpSert(int? courseId)
        {
            var course = await _courseService.GetCourseByIdAsync(courseId);
            if (course == null)
                course = new()
                {
                    CreatedAt = DateTime.UtcNow
                };
            else
            {
                string? userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                if (userId != course.InstructorId && !User.IsInRole("Admin"))
                    return Forbid();

            }

            return View(course);
        }


        [HttpPost]
        public async Task<IActionResult> Upsert(Course course,IFormFile? file)
        {
            if(!ModelState.IsValid)
            {

                return View(course);
            }

            //New Course
            if(course.Id == 0)
            {
                var courseAfterAdding = await _courseService.AddCourseAsync(course);
                course.Id = courseAfterAdding.Id;
            }

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string coursePath = @"Images/Courses/Course-" + course.Id.ToString();

            string finalPath = Path.Combine(wwwRootPath, coursePath);

            // Create directory if it doesn't exist
            Directory.CreateDirectory(finalPath);

            if(file is not null)
            {
                string extension = Path.GetExtension(file.FileName).ToLower();

                // Generate unique file name and save file
                string fileName = Guid.NewGuid().ToString() + extension;
                string filePath = Path.Combine(finalPath, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);

                }
                course.ImgUrl = @"/" + coursePath + "/" + fileName;

                await _courseService.UpdateCourseAsync(course);
            }

            return RedirectToAction(nameof(Index));

        }
    }
}
