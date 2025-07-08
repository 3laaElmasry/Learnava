using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess;
using Learnava.DataAccess.Data.Entities;
using Learnava.web.VMModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public CourseController(ICourseService courseService, IWebHostEnvironment webHostEnvironment, UserManager<ApplicationUser> userManager)
        {
            _courseService = courseService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!;
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);
            ViewData["instructorName"] = user?.FullName;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpSert(int? courseId)
        {
            var course = await _courseService.GetCourseByIdAsync(courseId);
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!;
            ApplicationUser? user = await _userManager.FindByIdAsync(userId);
            ViewData["instructorName"] = user?.FullName;

            if (course == null)
                course = new()
                {
                    CreatedAt = DateTime.UtcNow,
                    InstructorId = userId,
                    
                };
            else
            {

                if (userId != course.InstructorId && !User.IsInRole(SD.Role_Admin))
                {
                    return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
                };

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


        [HttpDelete]
        public async Task<IActionResult> Delete(int courseId)
        {
            var course = await _courseService.GetCourseByIdAsync(courseId);

            if (course is null)
            {
                return NotFound();
            }
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!;
            

            if ( course.InstructorId != userId || !User.IsInRole(SD.Role_Admin))
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }
            return View();
        }

        #region Api Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!;

            var user = await _userManager.FindByIdAsync(userId);

            IEnumerable<Course> courses;

            if (User.IsInRole(SD.Role_Admin))
            {
                courses = await _courseService.GetCoursesAsync(included:"Instructor");
            }
            else
            {
                courses = await _courseService.GetCoursesAsync(c => c.InstructorId == userId);
            }

            var result = courses.Select(c => new
            {
                c.Id,
                c.Title,
                c.Description,
                c.CreatedAt,
                instructorName = (c.Instructor is null) ? user!.FullName : c.Instructor.FullName,
                instructorEmail = (c.Instructor is null) ? user!.Email : c.Instructor.Email,
            });


            return Json(new { data = result });
        }
        #endregion
    }
}
