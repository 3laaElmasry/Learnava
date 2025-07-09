using System.Diagnostics;
using Learnava.BusinessLogic.IServiceContracts;
using Learnava.web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Learnava.web.Areas.User.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ICourseService _courseService;

        public HomeController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetCoursesAsync();
            return View(courses);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
