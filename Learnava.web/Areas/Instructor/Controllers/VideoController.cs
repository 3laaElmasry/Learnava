using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Learnava.web.Areas.Instructor.Controllers
{

    [Area("Instructor")]
    public class VideoController : Controller
    {
        public IActionResult Index(int courseId)
        {
            return View();
        }




    }
}
