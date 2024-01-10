using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalentDestination.DataAccess;

namespace TalentDestination.Controllers
{
	
    public class AdminController : Controller
    {
		private readonly talent_Context _context;

		public AdminController(talent_Context context)
		{
			_context = context;
		}

		[HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
		public async Task<IActionResult> Login(TblAdminLogin Model)
        {
			var user = await _context.TblAdminLogins
				.FirstOrDefaultAsync(u => u.Username == Model.Username && u.Password == Model.Password);


			if (user == null)
			{
				ViewBag.Message = "Invalid User Name or Password!";
				ModelState.Clear();
				return View();
			}
			else
			{
				HttpContext.Session.SetString("UserId", Convert.ToString(user.Id));
				HttpContext.Session.SetString("UserName", Convert.ToString(user.Username));
				ViewBag.UserLogin = Convert.ToString(user.Username);
                return RedirectToAction("Dashboard", "Home");

			}
		
		
		}
	}
}
