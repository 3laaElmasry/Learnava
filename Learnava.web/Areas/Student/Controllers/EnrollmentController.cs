using Learnava.BusinessLogic.IServiceContracts;
using Learnava.DataAccess;
using Learnava.DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Learnava.web.Areas.Student.Controllers
{ 
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public EnrollmentController(IEnrollmentService enrollmentService, UserManager<ApplicationUser> userManager)
        {
            _enrollmentService = enrollmentService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }



        #region Api calls
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
            var result = await Task.WhenAll(enrollments.Select(async e =>
            {
                var instructor = await _userManager.FindByIdAsync(e.Course!.InstructorId);
                var student = await _userManager.FindByIdAsync(e.Student!.ApplicationUserId);

                return new
                {
                    instructorName = instructor?.FullName,
                    instructorEmail = instructor?.Email,
                    studentName = student?.FullName,
                    studentEmail = student?.Email,
                    e.Id,
                    e.CourseId,
                    e.EnrollDate,
                    courseTitle = e.Course.Title
                };
            }));

            return Json(new { data = result });

        }
        #endregion

    }
}
