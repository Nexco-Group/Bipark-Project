using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Ronisim.Models;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Ronisim.Controllers
{
    public class apiController : Controller
    {
        private readonly ronisimContext _context;
        public apiController(ronisimContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult getcity(string provinveid)
        {
            try
            {
                string aresult = "<option></option>";

                var ssd = _context.City.Where(a => a.Province_Id == int.Parse(provinveid)).ToList();
                foreach (var gg in ssd)
                {
                    aresult += "<option value = '" + gg.Id + "'>" + gg.Name + "</option>";
                }

                return Json(new { result = aresult });
            }
            catch (Exception)
            {
                return Json(new { result = "مشکلی پیش آمده لطفا دوباره تلاش کنید" });
            }
        }

        [HttpGet]
        public ActionResult getschool(string cityId)
        {
            try
            {
                string iresult = "<option></option>";

                var isch = _context.Schools.Where(a => a.City_Id == int.Parse(cityId)).ToList();

                foreach (var sch in isch)
                {
                    iresult += "<option value = '" + sch.Id + "'>" + sch.SchoolNameFa + "</option>";
                }
                return Json(new { result = iresult });
            }
            catch (Exception)
            {
                return Json(new { result = "مشکلی پیش آمده لطفا دوباره تلاش کنید" });
            }
        }

        [HttpGet]
        public ActionResult getschoolinfo(string cityId)
        {
            try
            {
                string iresult = "<div class='table-responsive'> <table class='table table-bordered table-hover toggle-circle' id='footable'> <tr> <th data-name='logo'></th> <th data-name='namefa'>موسسه/آموزشگاه</th> <th data-name='nameen' data-breakpoints='xs'>Institute</th> <th data-breakpoints='xs sm' data-name='phone'>تلفن تماس</th> <th data-breakpoints='xs sm' data-name='address'>آدرس</th> </tr>";

                var isch = _context.Schools.Where(a => a.City_Id == int.Parse(cityId) && a.Active == true && a.Verified == true).ToList();

                if (isch == null || isch.Count == 0)
                {
                    var city = _context.City.Where(a => a.Id == int.Parse(cityId)).FirstOrDefault();
                    return Json(new { result = "<p style='text-align:center; font-weight:bold; color:black;font-size: 17px;background: white;'>در حال حاضر در شهر " + city.Name + " هیچ آموزشگاهی از وی سی وی استفاده نمی کند دوباره و در آینده نزدیک مراجعه کنید</p>" });
                }

                foreach (var sch in isch)
                {
                    iresult += "<tr>";
                    iresult += "<td>" + "<img class='avatar' src='" + new Service(_context).GetImageOfRole2(sch.Id, 20) + "'>" + " </td>";
                    iresult += "<td>" + sch.SchoolNameFa + "</td>";
                    iresult += "<td>" + sch.SchoolNameEn + "</td>";
                    iresult += "<td>" + sch.Phone + "</td>";
                    iresult += "<td>" + sch.Address + "</td>";
                    iresult += "</tr>";
                }
                iresult += "</table></div>";
                return Json(new { result = iresult });
            }
            catch (Exception)
            {
                return Json(new { result = "<p style='text-align:center; font-weight:bold; color:black;font-size: 17px;background: white;'>مشکلی پیش آمده لطفا دوباره تلاش کنید</p>" });
            }
        }

        [HttpGet]
        public ActionResult getteacher(string schoolId, string level1, string level2)
        {
            try
            {
                if (level1 == null || level1 == "") { return Json(new { result = "خطایی رخ داده است" }); }

                string iresult = "<option></option>";

                if (level2 == null || level2 == "")
                {

                    var cities = (from s in _context.Classes
                                  where s.SchoolId == int.Parse(schoolId) && s.Level == level1
                                  select s.TeacherId).Distinct().ToList();

                    foreach (var gg in cities)
                    {
                        var tchs = _context.Teachers.Where(a => a.Id == gg).FirstOrDefault();
                        iresult += "<option value = '" + gg + "'>" + Shared.ReturnGender(tchs.Sex) + tchs.LnameFa + "</option>";
                    }
                    if (cities == null || cities.Count == 0)
                    {
                        iresult = "<optgroup label = '" + "مدرسی پیدا نشد" + "'></optgroup>";
                    }
                }
                else
                {
                    var cities = (from s in _context.Classes
                                  where s.SchoolId == int.Parse(schoolId) && ((s.Level == level1 && s.Level2 == level2) || (s.Level == level2 && s.Level2 == level1))
                                  select s.TeacherId).Distinct().ToList();

                    foreach (var gg in cities)
                    {
                        var tchs = _context.Teachers.Where(a => a.Id == gg).FirstOrDefault();
                        iresult += "<option value = '" + gg + "'>" + Shared.ReturnGender(tchs.Sex) + tchs.LnameFa + "</option>";
                    }
                    if (cities == null || cities.Count == 0)
                    {
                        iresult = "<optgroup label = '" + "مدرسی پیدا نشد" + "'></optgroup>";
                    }
                }

                return Json(new { result = iresult });
            }
            catch (Exception)
            {
                return Json(new { result = "مشکلی پیش آمده لطفا دوباره تلاش کنید" });
            }
        }

        [HttpGet]
        public ActionResult getclasslist(string schoolId, string teacherId, string level1, string level2)
        {
            try
            {
                if (level1 == null || level1 == "") { return Json(new { result = "خطایی رخ داده است" }); }
                List<Classes> iclass = new List<Classes>();
                if (level2 == null || level2 == "")
                {
                    iclass = _context.Classes.Where(a => a.TeacherId == int.Parse(teacherId) && a.SchoolId == int.Parse(schoolId) && a.Level == level1).ToList();
                }
                else
                {
                    iclass = _context.Classes.Where(a => a.TeacherId == int.Parse(teacherId) && a.SchoolId == int.Parse(schoolId) && ((a.Level == level1 && a.Level2 == level2) || (a.Level == level2 && a.Level2 == level1))).ToList();
                }
                if (iclass == null || iclass.Count == 0) { return Json(new { result = "کلاسی پیدا نشد" }); }

                string finalresult = "";
                foreach (var classi in iclass)
                {
                    string tr0 = "<tr>";
                    string tr1 = "<tr>";
                    if (string.IsNullOrEmpty(classi.Level2))
                    {
                        tr1 += Getd(classi.Level);
                        tr1 += Getd(classi.LevelExtoEx);
                        tr0 += " <th class='text-center'>Level</th> <th class='text-center'>Lesson to Lesson</th> ";
                    }
                    else
                    {
                        tr1 += Getd(classi.Level);
                        tr1 += Getd(classi.LevelExtoEx);
                        tr1 += Getd(classi.Level2);
                        tr1 += Getd(classi.Level2ExtoEx);
                        tr0 += " <th class='text-center'>Level</th> <th class='text-center'>Lesson to Lesson</th>  <th class='text-center'>Level 2</th> <th class='text-center'>Lesson to Lesson 2</th>";
                    }
                    if (!string.IsNullOrEmpty(classi.Saturday))
                    {
                        tr1 += Getd(classi.Saturday);
                        tr0 += Geth("Saturday");
                    }
                    if (!string.IsNullOrEmpty(classi.Sunday))
                    {
                        tr1 += Getd(classi.Sunday);
                        tr0 += Geth("Sunday");
                    }
                    if (!string.IsNullOrEmpty(classi.Monday))
                    {
                        tr1 += Getd(classi.Monday);
                        tr0 += Geth("Monday");
                    }
                    if (!string.IsNullOrEmpty(classi.Tuesday))
                    {
                        tr1 += Getd(classi.Tuesday);
                        tr0 += Geth("Tuesday");
                    }
                    if (!string.IsNullOrEmpty(classi.Wednesday))
                    {
                        tr1 += Getd(classi.Wednesday);
                        tr0 += Geth("Wednesday");
                    }
                    if (!string.IsNullOrEmpty(classi.Thursday))
                    {
                        tr1 += Getd(classi.Thursday);
                        tr0 += Geth("Thursday");
                    }
                    if (!string.IsNullOrEmpty(classi.Friday))
                    {
                        tr1 += Getd(classi.Friday);
                        tr0 += Geth("Friday");
                    }

                    tr0 += Geth("<b>" + classi.ClassCode + " کلاس</b>");
                    tr1 += Getd("<input type='radio' class='to-labelauty-icon radiocus' id='cls_" + classi.Id + "' value='" + classi.Id + "' name='inputLableautyRadio' data-labelauty='عدم ثبت نام | ثبت نام در کلاس' /></td>");

                    tr0 += "</tr>";
                    tr1 += "</tr>";

                    finalresult += "<table class='table table-striped table-bordered'>" + tr0 + tr1 + "</table> <hr/>";
                }
                return Json(new { result = finalresult });
            }
            catch (Exception)
            {
                return Json(new { result = "مشکلی پیش آمده لطفا دوباره تلاش کنید" });
            }
        }

        private string Getd(string s) => "<td class='text-center'>" + s + "</td>";
        private string Geth(string s) => "<th class='text-center'>" + s + "</th>";


    }
}