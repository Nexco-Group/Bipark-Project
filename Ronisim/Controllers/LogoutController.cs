using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ronisim.Models;
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Ronisim.Controllers
{
    [Authorize]
    public class LogoutController : Controller
    {
        private readonly ronisimContext _context;

        public LogoutController(ronisimContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                if ((User == null) && !User.Identity.IsAuthenticated)
                {
                    return View();
                }

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                //HttpContext.Request.Host.ToString();
                return View();
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "MyProfile");
            }
        }
    }
}