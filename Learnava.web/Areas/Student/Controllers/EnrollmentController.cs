using Microsoft.AspNetCore.Mvc;

namespace Learnava.web.Areas.Student.Controllers
{
    public class EnrollmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
