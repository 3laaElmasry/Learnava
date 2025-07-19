using Learnava.DataAccess;
using Learnava.DataAccess.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Learnava.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =$"{SD.Role_Admin}")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
    
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> LockUnLock([FromBody] string? id)
        {
            if(id is null)
            {
                return Json(new { success = true, message = "Error While Locking / UnLocking" });
            }
            var userFromDb = await _userManager.FindByIdAsync(id);

            if (userFromDb == null)
            {
                return Json(new { success = true, message = "Error While Locking / UnLocking" });

            }

            if (userFromDb.LockoutEnd != null && userFromDb.LockoutEnd > DateTime.Now)
            {
                userFromDb.LockoutEnd = DateTime.Now;
               await _userManager.UpdateAsync(userFromDb);
                TempData["Success"] = "User is un locked successfully";
            }
            else
            {
                if(userFromDb.Role == SD.Role_Admin)
                {
                    return Json(new { success = true, message = "Error While Locking / UnLocking Admin Users Can't be Locked" });
                }
                userFromDb.LockoutEnd = DateTime.Now.AddYears(1);
                await _userManager.UpdateAsync(userFromDb);
                TempData["Success"] = "User is locked for 1 year successfully";

            }
            return Json(new { success = true, message = "Delete Success" });
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {

            var userList = _userManager.Users.Select(u => new
            {
                u.Id,
                u.Email,
                u.FullName,
                u.PhoneNumber,
                role = u.Role.Remove(0, 5),
                u.LockoutEnd
            });

            return Json(new { data = userList });
        }

        #endregion
    }
}
