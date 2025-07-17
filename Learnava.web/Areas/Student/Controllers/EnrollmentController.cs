using Microsoft.AspNetCore.Mvc;
using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess;
using Learnava.DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;


namespace Learnava.web.Areas.Student.Controllers
{

    [Area("Student")]
    [Authorize]
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStudentService _studentService;

        public EnrollmentController(IEnrollmentService enrollmentService, UserManager<ApplicationUser> userManager
            ,IStudentService studentService)
        {
            _enrollmentService = enrollmentService;
            _userManager = userManager;
            _studentService = studentService;
        }

        [Authorize(Roles = $"{SD.Role_Admin}, {SD.Role_Student}")]
        public IActionResult Index()
        {
            return View();
        }


        [Authorize(Roles =$"{SD.Role_Student}")]

        [HttpPost]
        public async Task<IActionResult> Create(int courseId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var student = await _studentService.GetStudentAsync(s => s.ApplicationUserId == userId);

            if(student == null)
            {
                return NoContent();
            }

            var alreadyEnrolled = await _enrollmentService.IsUserEnrolledAsync(courseId, student.Id);
            if (alreadyEnrolled)
            {
                TempData["Error"] = "You are already enrolled in this course.";
                return RedirectToAction(nameof(Index));
            }

            var enrollment = new Enrollment
            {
                CourseId = courseId,
                StudentId = student.Id,
                EnrollDate = DateTime.UtcNow
            };

            await _enrollmentService.CreateAsync(enrollment);

            return RedirectToAction(nameof(Index));

        }






        #region Api calls
        [Authorize(Roles = $"{SD.Role_Admin}, {SD.Role_Student}")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Enrollment> enrollments;
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value!;

            if (User.IsInRole(SD.Role_Admin))
            {
                enrollments = await _enrollmentService.GetEnrollmentsAsync(included: "Student,Course");
            }
            else
            {
                enrollments = await _enrollmentService
                    .GetEnrollmentsAsync(e => e.Student!.ApplicationUserId == userId, included: "Student,Course");
            }
            var result = new List<object>();
            foreach (var e in enrollments)
            {
                var instructor = await _userManager.FindByIdAsync(e.Course!.InstructorId);
                var student = await _userManager.FindByIdAsync(e.Student!.ApplicationUserId);

                result.Add(new
                {
                    instructorName = instructor?.FullName,
                    instructorEmail = instructor?.Email,
                    studentName = student?.FullName,
                    studentEmail = student?.Email,
                    e.Id,
                    e.CourseId,
                    e.EnrollDate,
                    courseTitle = e.Course.Title
                });
            }


            return Json(new { data = result });

        }

        [HttpDelete]
        [Authorize(Roles = $"{SD.Role_Admin},{SD.Role_Student}")]  
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var enrollment = await _enrollmentService.GetEnrollmentAsync(e => e.Id == id, included: "Student");

            if(enrollment is null)
            {
                return NotFound();
            }

            if(enrollment.Student!.ApplicationUserId != userId && !User.IsInRole(SD.Role_Admin))
            {
                return BadRequest();
            }
            var isDeleted = await _enrollmentService.DeleteEnrollmentAsync(id);
            return Json(new { success = (isDeleted is true) ? true : false, message = (isDeleted is true) ? "Delete Success" : "Deleted Fail" });

        }
        #endregion

    }
}