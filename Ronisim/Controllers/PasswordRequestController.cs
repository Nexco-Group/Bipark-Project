using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Ronisim.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ronisim.Controllers
{
    public class PasswordRequestController : Controller
    {

        private readonly ronisimContext _context;
        public PasswordRequestController(ronisimContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if ((User != null) && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "MyProfile");
            }

            HttpContext.Session.Remove("PRCaptcha");
            HttpContext.Session.SetString("PRCaptcha", Shared.GetSMSCode());
            HttpContext.Session.Remove("PRMessage");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PasswordRequestStep1ViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (1==1)
                    {
                        var iUser = await (from s in _context.iUser
                                                where s.Mobile == vm.Mobile
                                                select s)
                                                .FirstOrDefaultAsync();

                        if (iUser != null)
                        {
                            string SMS_Code = Shared.GetSMSCode();
                            string sms = "رونیسیم" + "\n" + "کد " + SMS_Code + " را جهت بازیابی رمز عبور وارد کنید";
                            var result =  Shared.SendSMS(iUser.Mobile, sms);
                            if (result)
                            {
                                HttpContext.Session.Remove("PRSMS_Code");
                                HttpContext.Session.SetString("PRSMS_Code", SMS_Code);

                                HttpContext.Session.SetString("Mobile", iUser.Mobile);
                                return Json(new { result = 1 });
                            }
                            else
                            {
                                HttpContext.Session.Remove("PRCaptcha");
                                HttpContext.Session.SetString("PRCaptcha", Shared.GetSMSCode());
                                return Json(new { result = "ارسال پیامک با مشکل مواجه شد. لطفا با کمی تاخیر و یا دوباره تاخیر تلاش کنید" });
                            }
                        }
                        else
                        {
                            HttpContext.Session.Remove("PRCaptcha");
                            HttpContext.Session.SetString("PRCaptcha", Shared.GetSMSCode());
                            return Json(new { result = "این نام کاربری (شماره موبایل) در سیستم ثبت نشده است" });
                        }
                    }
                    else
                    {
                        HttpContext.Session.Remove("PRCaptcha");
                        HttpContext.Session.SetString("PRCaptcha", Shared.GetSMSCode());
                        return Json(new { result = "عدد تصویر امنیتی اشتباه می باشد" });
                    }
                }
                else
                {
                    HttpContext.Session.Remove("PRCaptcha");
                    HttpContext.Session.SetString("PRCaptcha", Shared.GetSMSCode());
                    var Message = string.Join(" - ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                    return Json(new { result = Message });
                }
            }
            catch (Exception)
            {
                HttpContext.Session.Remove("PRCaptcha");
                HttpContext.Session.SetString("PRCaptcha", Shared.GetSMSCode());
                return Json(new { result = "مشکلی پیش آمده است لطفا دوباره تلاش کنید" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Verify(PasswordRequestStep1ViewModel vm, string SMSCode)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (1==1)
                    {
                        //HttpContext.Session.Remove("PRCaptcha");
                        var iUser = await (from s in _context.iUser
                                                where s.Mobile == vm.Mobile
                                                select s)
                                                .FirstOrDefaultAsync();

                        if (iUser != null)
                        {
                            if (HttpContext.Session.GetString("PRSMS_Code") == SMSCode)
                            {
                                HttpContext.Session.SetString("Mobile", iUser.Mobile);
                                HttpContext.Session.SetInt32("Step", 2);
                                return Json(new { result = 1 });
                            }
                            else
                            {
                                var ss = HttpContext.Session.GetString("PRSMS_Code");
                                return Json(new { result = 2 });
                            }
                        }
                        else
                        {
                            HttpContext.Session.Remove("PRCaptcha");
                            HttpContext.Session.SetString("PRCaptcha", Shared.GetSMSCode());
                            return Json(new { result = "این نام کاربری (شماره موبایل) در سیستم ثبت نشده است" });
                        }
                    }
                    else
                    {
                        HttpContext.Session.Remove("PRCaptcha");
                        HttpContext.Session.SetString("PRCaptcha", Shared.GetSMSCode());
                        return Json(new { result = "عدد تصویر امنیتی اشتباه می باشد" });
                    }
                }
                else
                {
                    HttpContext.Session.Remove("PRCaptcha");
                    HttpContext.Session.SetString("PRCaptcha", Shared.GetSMSCode());

                    var Message = string.Join(" - ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                    return Json(new { result = Message });
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.Remove("PRCaptcha");
                HttpContext.Session.SetString("PRCaptcha", Shared.GetSMSCode());
                return Json(new { result = "مشکلی پیش آمده است لطفا دوباره تلاش کنید" + ex.ToString() });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReSend(string Mobile)
        {
            try
            {                
                string SMS_Code = Shared.GetSMSCode();
                string sms = "رونیسیم" + "\n" + "کد " + SMS_Code + " را جهت بازیابی رمز عبور وارد کنید";
                var result = Shared.SendSMS(Mobile, sms);
                if (result)
                {
                    HttpContext.Session.Remove("PRSMS_Code");
                    HttpContext.Session.SetString("PRSMS_Code", SMS_Code);

                    return Json(new { result = 1 });
                }
                else
                {
                    return Json(new { result = "ارسال پیامک با مشکل مواجه شد. لطفا با کمی تاخیر و یا دوباره تاخیر تلاش کنید" });
                }
            }
            catch (Exception)
            {
                return Json(new { result = "مشکلی پیش آمده است لطفا دوباره تلاش کنید" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(PasswordRequestStep2ViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mobile = HttpContext.Session.GetString("Mobile");
                    var studentToUpdate = await _context.iUser.SingleOrDefaultAsync(s => s.Mobile == mobile);

                    string Saltforpass = Shared.GetSalt();
                    studentToUpdate.Salt = Saltforpass;
                    studentToUpdate.PassWord = Shared.GetHash(vm.Password + Saltforpass);

                    await _context.SaveChangesAsync();

                    HttpContext.Session.SetInt32("Step", 3);
                    HttpContext.Session.Remove("Mobile");
                    HttpContext.Session.Remove("PRMessage");
                }
                else
                {
                    var Message = string.Join(" - ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                    HttpContext.Session.SetString("PRMessage", Message);
                }
            }
            catch (Exception)
            {
                HttpContext.Session.SetString("PRMessage", "مشکلی پیش آمده است لطفا دوباره تلاش کنید");
            }
            return RedirectToAction("Index", "PasswordRequest");
        }
    }
}