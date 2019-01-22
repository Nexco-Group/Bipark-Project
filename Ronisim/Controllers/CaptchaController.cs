using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Ronisim.Controllers
{
    public class CaptchaController : Controller
    {
        [Route("Captcha/PRCaptcha.jpg")]
        public ActionResult PRCaptcha()
        {
            var captcha = HttpContext.Session.GetString("PRCaptcha");
            if (captcha != null)
            {
                return Content("error");

                //return File(Shared.GenerateCaptcha(captcha), "image/jpeg");
            }
            else
            {
                return Content("error");
            }
        }


        [Route("Profile.jpg")]
        public ActionResult Profile()
        {
            var Image = HttpContext.Session.GetString("ProfileImage");
            if (Image != null)
            {
                return Content("error");

                // return File(Shared.GenerateCaptcha(Image), "image/jpeg");
            }
            else
            {
                return Content("error");

                //return File(Shared.GenerateCaptcha(Image), "image/jpeg");
            }
        }

        [Route("Captcha/JoinCaptcha.jpg")]
        public ActionResult JoinCaptcha()
        {
            var captcha = HttpContext.Session.GetString("JoinCaptcha");
            if (captcha != null)
            {
                return Content("error");

                //return File(Shared.GenerateCaptcha(captcha), "image/jpeg");
            }
            else
            {
                return Content("error");
            }
        }

        [Route("Captcha/LoginCaptcha.jpg")]
        public ActionResult LoginCaptcha()
        {
            var captcha = HttpContext.Session.GetString("LoginCaptcha");
            if (captcha != null)
            {
                return Content("error");

                //return File(Shared.GenerateCaptcha(captcha), "image/jpeg");
            }
            else
            {
                return Content("error");
            }
        }
    }
}