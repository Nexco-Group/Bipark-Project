using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ronisim.Models;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Ronisim.Controllers
{
    public class LoginController : Controller
    {
        private readonly ronisimContext _context;

        public LoginController(ronisimContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(string ReturnUrl)
        {
            if ((HttpContext.User != null) && User.Identity.IsAuthenticated)
            {
                if (Url.IsLocalUrl(ReturnUrl))
                    return Redirect(ReturnUrl);
                else
                    return RedirectToAction("Index", "Management");
            }
            //HttpContext.Session.Remove("LoginCaptcha");
            //HttpContext.Session.SetString("LoginCaptcha", Shared.GetSMSCode());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            try
            {
                //if (HttpContext.Session.GetString("LoginCaptcha").Equals(model.Captcha))
                //{
                //HttpContext.Session.Remove("LoginCaptcha");
                if (ModelState.IsValid)
                {
                    var iUser = await (from s in _context.Admins
                                       where s.Mobile == model.Mobile && s.PassWord == Shared.GetHash(model.Password + s.Salt)
                                       select s)
                                      .FirstOrDefaultAsync();
                    var mob = "";
                    var role = "";
                    if (iUser != null)
                    {
                        role = "admin";
                        mob = iUser.Mobile;
                    }
                    else
                    {
                        var iUser2 = await (from s in _context.iUser
                                            where s.Mobile == model.Mobile && s.PassWord == Shared.GetHash(model.Password + s.Salt)
                                            select s)
                                      .FirstOrDefaultAsync();

                        if (iUser2 != null)
                        {
                            if (iUser2.Active != true || iUser2.Deleted == true)
                            {
                                return Json(new { result = "این کاربر غیر فعال میباشد" });
                            }
                            else
                            {
                                if (iUser2.Verify != true)
                                {
                                    return Json(new { result = "شما هنوز توسط ادمین تایید نشده اید" });
                                }
                                mob = iUser2.Mobile;
                                role = "iUser";
                            }
                        }
                        else
                        {
                            return Json(new { result = "رمز ورود و یا نام کاربری اشتباه میباشد" });

                        }
                        //HttpContext.Session.Remove("LoginCaptcha");
                        //HttpContext.Session.SetString("LoginCaptcha", Shared.GetSMSCode());

                    }
                    var claims = new List<Claim>(){
                        new Claim(ClaimTypes.Name, mob)
                    };
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    if (model.RememberMe)
                    {
                        var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.UtcNow.AddDays(30),
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    }
                    else
                    {
                        var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.UtcNow.AddHours(4),
                            IsPersistent = true
                        };

                        await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    }
                    return Json(new { result = "1" });
                }
                else
                {
                    var Message = string.Join(" - ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
                    return Json(new { result = Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "Encountered a problem. Try again." + ex });
            }
        }
    }
}