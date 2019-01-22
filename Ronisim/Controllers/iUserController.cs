using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ronisim.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static Ronisim.Controllers.ManagementController;
using SelectPdf;
using OfficeOpenXml;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Ronisim.Controllers
{
    //[Authorize]
    public class iUserController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly ronisimContext _context;
        //static string rooturl = @"C:\\Website\\Contents";
        public iUserController(ronisimContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                string mobile = User.Identity.Name;
                var iUser = (from s in _context.iUser
                             where s.Mobile == mobile && s.Active == true && s.Deleted != true
                             select s).FirstOrDefault();
                if (iUser == null)
                {
                    return RedirectToAction("Index", "Logout");
                }

                if (HttpContext.Session.GetString("orderSuccess") != null)
                {
                    ViewBag.orderSuccess = HttpContext.Session.GetString("orderSuccess").ToString();
                    HttpContext.Session.Remove("orderSuccess");
                }

                if (HttpContext.Session.GetString("orderMes") != null)
                {
                    ViewBag.orderMes = HttpContext.Session.GetString("orderMes").ToString();
                    HttpContext.Session.Remove("orderMes");
                }

                PassToMyProfile PassToView = new PassToMyProfile()
                {
                    iUser = iUser,
                    balance = await GetBalance(iUser.Id, "1"),
                    iNews = await (from s in _context.News
                                   where s.Active == true && s.iUser == true
                                   orderby s.Id descending
                                   select s).ToListAsync()
                };

                //var rq = await (from s in _context.Product
                //                where s.Active == true && s.Deleted != true
                //                orderby s.Id descending
                //                select s).ToListAsync();

                //foreach (var r in rq)
                //{
                //    r.Category = await getpack(r.Id);
                //}
                //PassToView.iProduct = rq;
                //List<Login> iLogin = await (from s in _context.Login
                //                            orderby s.Id descending
                //                            where s.Who_Id == iUser.Id && s.Who == "10"
                //                            select s).Take(5).ToListAsync();
                //if (iLogin != null)
                //{
                //    PassToView.iLogin = iLogin;
                //}
                //var mycode = 10;

                //if (iUser.AccessLevel == "1")
                //{
                //    mycode = 0;
                //}

                //ViewBag.MyImage = new Service(_context).GetImageOfRole(iUser.Id, mycode);

                //PassToView.iMessage = new Service(_context).getNewMessage(iUser.Id, mycode.ToString());
                //PassToView.iNotification = Service.Getsystemnotification();

                return View(PassToView);

            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> New()
        {
            try
            {
                string mobile = User.Identity.Name;
                var iUser = (from s in _context.iUser
                             where s.Mobile == mobile && s.Active == true && s.Deleted != true
                             select s).FirstOrDefault();
                if (iUser == null)
                {
                    return RedirectToAction("Index", "Logout");
                }

                if (HttpContext.Session.GetString("orderSuccess") != null)
                {
                    ViewBag.orderSuccess = HttpContext.Session.GetString("orderSuccess").ToString();
                    HttpContext.Session.Remove("orderSuccess");
                }

                if (HttpContext.Session.GetString("orderMes") != null)
                {
                    ViewBag.orderMes = HttpContext.Session.GetString("orderMes").ToString();
                    HttpContext.Session.Remove("orderMes");
                }

                PassToMyProfile PassToView = new PassToMyProfile()
                {
                    iUser = iUser,
                    balance = await GetBalance(iUser.Id, "1"),
                    iNews = await (from s in _context.News
                                   where s.Active == true && s.iUser == true
                                   orderby s.Id descending
                                   select s).ToListAsync()
                };

                PassToView.iProduct = await (from s in _context.Product
                                             where s.Active == true && s.Deleted != true
                                             orderby s.Id descending
                                             select s).ToListAsync();

                return View("New", PassToView);

            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> getRmoney()
        {
            try
            {
                string mobile = User.Identity.Name;
                var rs = (from s in _context.iUser
                          where s.Mobile == mobile
                          select s).FirstOrDefault();

                var jj = await (from s in _context.Balance
                                where s.WhoId == rs.Id && s.Who == "1"
                                orderby s.Id descending
                                select s).ToListAsync();


                if (jj == null || jj.Count == 0)
                {
                    return Json(new { result = 0 });
                }
                else
                {
                    return Json(new { result = jj });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = 0 });
            }
        }


        public string getdype(int? s)
        {
            if (s == 1)
            {
                return "روز";
            }
            else if (s == 2)
            {
                return "هفته";
            }
            else if (s == 3)
            {
                return "ماه";
            }
            else
            {
                return "سال";
            }

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> getbal()
        {
            string mobile = User.Identity.Name;
            var rs = (from s in _context.iUser
                      where s.Mobile == mobile
                      select s).FirstOrDefault();


            var PassToView = new getbalss()
            {
                a1 = await GetBalance(rs.Id, "1")
            };

            return Json(new { result = PassToView });

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Requests()
        {
            try
            {
                string mobile = User.Identity.Name;
                var iUser = (from s in _context.iUser
                             where s.Mobile == mobile && s.Active == true && s.Deleted != true
                             select s).FirstOrDefault();
                if (iUser == null)
                {
                    return RedirectToAction("Index", "Logout");
                }

                var PassToView = new PassToMyRequesta()
                {
                    iUser = iUser,
                    iNews = await (from s in _context.News
                                   where s.Active == true && s.iUser == true
                                   orderby s.Id descending
                                   select s).ToListAsync()
                };

                return View(PassToView);

            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }


        [HttpGet]
        public async Task<IActionResult> getRequest(string fromdate, string todate, string formobile, int? status, int? size, int? sizetype, int? whoid)
        {
            string mobile = User.Identity.Name;
            var iUser = (from s in _context.iUser
                         where s.Mobile == mobile && s.Active == true && s.Deleted != true
                         select s).FirstOrDefault();
            if (iUser == null)
            {
                return RedirectToAction("Index", "Logout");
            }

            var rq = new List<Request>();
            var who = "1";
            whoid = iUser.Id;

            if (string.IsNullOrEmpty(fromdate) && string.IsNullOrEmpty(todate))
            {
                rq = await (from s in _context.Request
                            where
                            s.Status > 0 && (Shared.PersianTimeDiff2(s.OrderDate, Shared.GetDate()) <= 1)
                            && (status == null ? true : s.Status == status)
                            && (whoid == null ? true : s.WhoId == whoid)
                            && (size == null ? true : _context.Product.Any(a => a.Id == s.PackId))
                            orderby s.Id descending
                            select s).ToListAsync();
            }
            else
            {
                if (string.IsNullOrEmpty(fromdate))
                {
                    rq = await (from s in _context.Request
                                where s.Status > 0 && (Shared.PersianTimeDiff2(s.OrderDate, todate) >= 0)
                            && (status == null ? true : s.Status == status)
                            && (whoid == null ? true : s.WhoId == whoid)
                                && (size == null ? true : _context.Product.Any(a => a.Id == s.PackId))
                                orderby s.Id descending
                                select s).ToListAsync();
                }
                else if (string.IsNullOrEmpty(todate))
                {
                    rq = await (from s in _context.Request
                                where s.Status > 0 && (Shared.PersianTimeDiff2(fromdate, s.OrderDate) >= 0)
                            && (status == null ? true : s.Status == status)
                            && (whoid == null ? true : s.WhoId == whoid)
                                && (size == null ? true : _context.Product.Any(a => a.Id == s.PackId))
                                orderby s.Id descending
                                select s).ToListAsync();
                }
                else
                {
                    if ((Shared.PersianTimeDiff2(fromdate, todate) < 0))
                    {
                        return Json(new
                        {
                            result = 1
                        });
                    }
                    rq = await (from s in _context.Request
                                where s.Status > 0 && (Shared.PersianTimeDiff2(fromdate, s.OrderDate) >= 0) && (Shared.PersianTimeDiff2(s.OrderDate, todate) >= 0)
                            && (status == null ? true : s.Status == status)
                            && (whoid == null ? true : s.WhoId == whoid)
                                && (size == null ? true : _context.Product.Any(a => a.Id == s.PackId))
                                orderby s.Id descending
                                select s).ToListAsync();
                }
            }
            if (rq == null || rq.Count == 0)
            {
                return Json(new
                {
                    result = 0
                });
            }

            float totalgig = 0;
            var totalmoney = 0;

            var res = new List<Request_plus>();
            foreach (var r in rq)
            {
                var pk = await (from s in _context.Product
                                where s.Id == r.PackId
                                select s).FirstOrDefaultAsync();
                var nam = "";
                var cod = 10000001;

                var user = await (from s in _context.iUser
                                  where s.Id == r.WhoId
                                  select s).FirstOrDefaultAsync();
                nam = user.Fname + " " + user.Lname + " - کاربر";
                cod = user.Code.Value;


                var pack = await (from s in _context.Product
                                  where s.Id == r.PackId
                                  select s).FirstOrDefaultAsync();

                totalmoney = r.TotalPrice.Value;


                res.Add(new Request_plus
                {
                    FromDate = r.FromDate,
                    FromTime = r.FromTime,
                    ToDate = r.ToDate,
                    ToTime = r.ToTime,
                    st = r.Status.Value,
                    gig = totalgig,
                    money = r.TotalPrice.Value,
                    money2 = pack.Price2,
                    whoCode = cod,
                    Id = r.Id,
                    Pack = await getpack(r.PackId),
                    Active = r.Active,
                    OrderDate = r.OrderDate + " - " + r.OrderTime,
                    RequestCode = r.RequestCode,
                    Status = GetStatus(r.Status.Value),
                    Who = nam,
                    note = r.Note,

                });
            }
            return Json(new
            {
                result = res
            });
        }

        public string GetStatus(int a)
        {
            if (a == 0)
            {
                return "لغو شده توسط نمابنده";
            }
            else if (a == 1 || a == 10)
            {
                return "تایید نشده توسط ادمین";
            }
            else if (a == 2)
            {
                return "تایید شده";
            }
            else if (a == 4)
            {
                return "لغو شده توسط ادمین";
            }
            return "نامشخص";
        }



        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Transaction()
        {
            try
            {
                string mobile = User.Identity.Name;
                var rs = (from s in _context.iUser
                          where s.Mobile == mobile && s.Active == true && s.Deleted != true
                          select s).FirstOrDefault();
                if (rs == null)
                {
                    return RedirectToAction("Index", "Logout");
                }


                //if (jj == null || jj.Count == 0)
                //{
                //    return Json(new { result = 0 });
                //}
                //else
                //{
                //    return Json(new { result = jj });
                //}                

                var PassToView = new PassToMyTransaction()
                {
                    iUser = rs,
                    iNews = await (from s in _context.News
                                   where s.Active == true && s.iUser == true
                                   orderby s.Id descending
                                   select s).ToListAsync()
                };

                return View("Transaction", PassToView);

            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }


        [HttpGet]
        public async Task<IActionResult> getAllmoney(string fromdate, string todate, int? status, int? whoid)
        {
            try
            {

                string mobile = User.Identity.Name;
                var iUser = (from s in _context.iUser
                             where s.Mobile == mobile && s.Active == true && s.Deleted != true
                             select s).FirstOrDefault();
                if (iUser == null)
                {
                    return RedirectToAction("Index", "Logout");
                }

                var jj = new List<Balance>();
                var who = "1";
                whoid = iUser.Id;

                if (string.IsNullOrEmpty(fromdate) && string.IsNullOrEmpty(todate))
                {
                    jj = await (from s in _context.Balance
                                where (Shared.PersianTimeDiff2(s.Date, Shared.GetDate()) <= 1)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                orderby s.Id descending
                                select s).ToListAsync();
                }
                else
                {
                    if (string.IsNullOrEmpty(fromdate))
                    {
                        jj = await (from s in _context.Balance
                                    where (Shared.PersianTimeDiff2(s.Date, todate) >= 0)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                    orderby s.Id descending
                                    select s).ToListAsync();
                    }
                    else if (string.IsNullOrEmpty(todate))
                    {
                        jj = await (from s in _context.Balance
                                    where (Shared.PersianTimeDiff2(fromdate, s.Date) >= 0)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                    orderby s.Id descending
                                    select s).ToListAsync();
                    }
                    else
                    {
                        if ((Shared.PersianTimeDiff2(fromdate, todate) < 0))
                        {
                            return Json(new
                            {
                                result = 1
                            });
                        }

                        jj = await (from s in _context.Balance
                                    where (Shared.PersianTimeDiff2(fromdate, s.Date) >= 0) && (Shared.PersianTimeDiff2(s.Date, todate) >= 0)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                    orderby s.Id descending
                                    select s).ToListAsync();
                    }
                }

                if (jj == null || jj.Count == 0)
                {
                    return Json(new { result = 0 });
                }
                else
                {
                    //foreach(var ds in jj)
                    //{
                    //    if(ds.Amount < 0)
                    //    {
                    //        if (ds.RequestId != null && ds.RequestId != 0)
                    //        {
                    //            var rq = await (from s in _context.Request
                    //                            where s.Id == ds.RequestId
                    //                            select s).FirstOrDefaultAsync();
                    //            ds.Description = "بابت رزرو با کد پیگیری" + rq.RequestCode;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        ds.Description = "افزایش موجودی توسط درگاه بانکی";
                    //    }
                    //}
                    return Json(new { result = jj });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = 0 });
            }
        }

        [HttpGet]
        public async Task<IActionResult> getAllmoneyExcel(string fromdate, string todate, int? status, int? whoid)
        {
            try
            {
                string mobile = User.Identity.Name;
                var iUser = (from s in _context.iUser
                             where s.Mobile == mobile && s.Active == true && s.Deleted != true
                             select s).FirstOrDefault();
                if (iUser == null)
                {
                    return RedirectToAction("Index", "Logout");
                }

                var jj = new List<Balance>();
                var who = "1";
                whoid = iUser.Id;


                if (string.IsNullOrEmpty(fromdate) && string.IsNullOrEmpty(todate))
                {
                    jj = await (from s in _context.Balance
                                where (Shared.PersianTimeDiff2(s.Date, Shared.GetDate()) <= 1)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                orderby s.Id descending
                                select s).ToListAsync();
                }
                else
                {
                    if (string.IsNullOrEmpty(fromdate))
                    {
                        jj = await (from s in _context.Balance
                                    where (Shared.PersianTimeDiff2(s.Date, todate) >= 0)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                    orderby s.Id descending
                                    select s).ToListAsync();
                    }
                    else if (string.IsNullOrEmpty(todate))
                    {
                        jj = await (from s in _context.Balance
                                    where (Shared.PersianTimeDiff2(fromdate, s.Date) >= 0)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                    orderby s.Id descending
                                    select s).ToListAsync();
                    }
                    else
                    {
                        if ((Shared.PersianTimeDiff2(fromdate, todate) < 0))
                        {
                            return Json(new
                            {
                                result = 1
                            });
                        }

                        jj = await (from s in _context.Balance
                                    where (Shared.PersianTimeDiff2(fromdate, s.Date) >= 0) && (Shared.PersianTimeDiff2(s.Date, todate) >= 0)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                    orderby s.Id descending
                                    select s).ToListAsync();
                    }
                }

                var total = 0;
                var inc = 0;
                var outc = 0;

                foreach (var j in jj)
                {
                    total += j.Amount.Value;
                    if (j.Amount > 0)
                    {
                        inc += j.Amount.Value;
                    }
                    else
                    {
                        outc += j.Amount.Value;
                    }
                }

                if (jj == null || jj.Count == 0)
                {
                    return Json(new { result = 0 });
                }
                else
                {
                    string sWebRootFolder = _hostingEnvironment.WebRootPath;
                    string sFileName = @"Excel/AllRequest-" + Shared.GetDate().Replace('/', '-') + "-" + RandomString(8) + @".xlsx";
                    string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
                    FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                    if (file.Exists)
                    {
                        file.Delete();
                        file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                    }
                    using (ExcelPackage package = new ExcelPackage(file))
                    {

                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Request");
                        //First add the headers                       
                        worksheet.Cells[1, 1].Value = "شماره پیگیری";
                        worksheet.Cells[1, 2].Value = "مبلغ";
                        worksheet.Cells[1, 3].Value = "نوع تراکنش";
                        worksheet.Cells[1, 4].Value = "توضیح تراکنش";
                        worksheet.Cells[1, 5].Value = "وضعیت";
                        worksheet.Cells[1, 6].Value = "تاریخ";
                        worksheet.Cells[1, 7].Value = "زمان";

                        // Inserts Data
                        for (int i = 0; i < jj.Count(); i++)
                        {
                            var obj = jj[i];
                            worksheet.Cells[i + 2, 1].Value = obj.Id;
                            worksheet.Cells[i + 2, 2].Value = obj.Amount;
                            if (obj.Amount > 0)
                            {
                                worksheet.Cells[i + 2, 3].Value = "واریز مبلغ - افزایش موجودی";
                            }
                            else
                            {
                                worksheet.Cells[i + 2, 3].Value = "کسر مبلغ - بابت رزرو";
                            }
                            worksheet.Cells[i + 2, 4].Value = obj.Description;
                            if (obj.Consider == false || obj.Active == false)
                            {
                                worksheet.Cells[i + 2, 5].Value = "ناموفق - لغو شده - بررسی نشده";
                            }
                            else
                            {
                                worksheet.Cells[i + 2, 5].Value = "موفق - حساب شده در موجودی";
                            }
                            worksheet.Cells[i + 2, 6].Value = obj.Date;
                            worksheet.Cells[i + 2, 7].Value = obj.Time;
                        }
                        var ss = jj.Count() + 2;
                        worksheet.Cells[ss + 1, 3].Value = "ورودی";
                        worksheet.Cells[ss + 1, 4].Value = "خروجی";
                        worksheet.Cells[ss + 1, 2].Value = "جمع مبالغ";


                        worksheet.Cells[ss + 2, 3].Value = inc;
                        worksheet.Cells[ss + 2, 4].Value = outc;
                        worksheet.Cells[ss + 2, 2].Value = total;

                        package.Save(); //Save the workbook.
                    }
                    return Json(new
                    {
                        result = URL
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = 0 });
            }
        }


        [HttpGet]
        public async Task<IActionResult> getAllmoneyPDF(string fromdate, string todate, int? status, int? whoid)
        {
            try
            {
                string mobile = User.Identity.Name;
                var iUser = (from s in _context.iUser
                             where s.Mobile == mobile && s.Active == true && s.Deleted != true
                             select s).FirstOrDefault();
                if (iUser == null)
                {
                    return RedirectToAction("Index", "Logout");
                }

                var jj = new List<Balance>();
                var who = "1";
                whoid = iUser.Id;


                if (string.IsNullOrEmpty(fromdate) && string.IsNullOrEmpty(todate))
                {
                    jj = await (from s in _context.Balance
                                where (Shared.PersianTimeDiff2(s.Date, Shared.GetDate()) <= 1)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                orderby s.Id descending
                                select s).ToListAsync();
                }
                else
                {
                    if (string.IsNullOrEmpty(fromdate))
                    {
                        jj = await (from s in _context.Balance
                                    where (Shared.PersianTimeDiff2(s.Date, todate) >= 0)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                    orderby s.Id descending
                                    select s).ToListAsync();
                    }
                    else if (string.IsNullOrEmpty(todate))
                    {
                        jj = await (from s in _context.Balance
                                    where (Shared.PersianTimeDiff2(fromdate, s.Date) >= 0)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                    orderby s.Id descending
                                    select s).ToListAsync();
                    }
                    else
                    {
                        if ((Shared.PersianTimeDiff2(fromdate, todate) < 0))
                        {
                            return Json(new
                            {
                                result = 1
                            });
                        }

                        jj = await (from s in _context.Balance
                                    where (Shared.PersianTimeDiff2(fromdate, s.Date) >= 0) && (Shared.PersianTimeDiff2(s.Date, todate) >= 0)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                    orderby s.Id descending
                                    select s).ToListAsync();
                    }
                }

                var total = 0;
                var inc = 0;
                var outc = 0;

                foreach (var j in jj)
                {
                    total += j.Amount.Value;
                    if (j.Amount > 0)
                    {
                        inc += j.Amount.Value;
                    }
                    else
                    {
                        outc += j.Amount.Value;
                    }
                }

                if (jj == null || jj.Count == 0)
                {
                    return Json(new { result = 0 });
                }
                else
                {
                    string sFileName2 = @"PDF/AllRequest-" + Shared.GetDate().Replace('/', '-') + "-" + RandomString(8) + @".pdf";
                    string sFileName = @"wwwroot/" + sFileName2;

                    string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName2);

                    string HtmlContent = Service.GetHTMLString3(jj, total, inc, outc);

                    HtmlToPdf converter = new HtmlToPdf();

                    // set converter options
                    converter.Options.PdfPageSize = PdfPageSize.A4;
                    converter.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
                    //converter.Options.WebPageWidth = 1524;
                    //converter.Options.WebPageHeight = webPageHeight;
                    converter.Options.DisplayHeader = false;

                    // create a new pdf document converting an html string
                    PdfDocument doc = converter.ConvertHtmlString(HtmlContent, "PDF");

                    // save pdf document
                    doc.Save(sFileName);

                    // close pdf document
                    doc.Close();


                    //return Ok("Successfully created PDF document.");

                    return Json(new
                    {
                        result = URL
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = 0 });
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            try
            {
                return View("Register");
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("FnameFa,LnameFa,Mobile,Password,ConfirmPassword,GenderInput,Plate")] JoinViewModel students)
        {
            string Message = "";
            if (ModelState.IsValid)
            {
                //if ((!string.IsNullOrEmpty(Captcha)) && HttpContext.Session.GetString("JoinCaptcha").Equals(Captcha))
                //{
                //HttpContext.Session.Remove("JoinCaptcha");
                //HttpContext.Session.SetString("JoinCaptcha", Shared.GetSMSCode());

                string mobile = students.Mobile;

                var sexist = await (from s in _context.iUser where s.Mobile == mobile select s).FirstOrDefaultAsync();
                if (sexist != null)
                {
                    Message = "این شماره موبایل در سیستم وجود دارد لطفا شماره موبایل دیگری وارد کنید";

                    return Json(new { result = Message });
                }

                bool Gender = students.GenderInput;

                string Saltforpass = Shared.GetSalt();
                iUser newone = new iUser()
                {
                    Fname = Shared.Ok_String(students.FnameFa),
                    Lname = Shared.Ok_String(students.LnameFa),
                    Sex = Gender,
                    JoiningDate = Shared.GetDate(),
                    Salt = Saltforpass,
                    Mobile = mobile,
                    UserName = mobile,
                    PassWord = Shared.GetHash(students.Password + Saltforpass),
                    Active = true,
                    Verify = false,
                    Plate = students.Plate
                };

                await _context.AddAsync(newone);
                await _context.SaveChangesAsync();

                newone.Code = newone.Id + 1000;
                await _context.SaveChangesAsync();

                return Json(new { result = 1 });

                //var claims = new List<Claim>(){
                //        new Claim(ClaimTypes.Name, mobile)
                //    };
                //var claimsIdentity = new ClaimsIdentity(
                //    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties
                //{
                //    ExpiresUtc = DateTime.UtcNow.AddHours(4),
                //    IsPersistent = true
                //};

                //await HttpContext.SignInAsync(
                //CookieAuthenticationDefaults.AuthenticationScheme,
                //new ClaimsPrincipal(claimsIdentity),
                //authProperties);

                //return Json(new { result = 1 });

                //}
                //else
                //{
                //    //HttpContext.Session.Remove("JoinCaptcha");
                //    //HttpContext.Session.SetString("JoinCaptcha", Shared.GetSMSCode());

                //    Message = "عدد تصویر امنیتی درست نمی باشد";
                //    return Json(new { result = Message });
                //}
            }
            else
                //{
                //    HttpContext.Session.Remove("JoinCaptcha");
                //    HttpContext.Session.SetString("JoinCaptcha", Shared.GetSMSCode());
                Message = string.Join(" - ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
            return Json(new { result = Message });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> getOlist()
        {
            try
            {
                string mobile = User.Identity.Name;
                var iuser = await (from s in _context.iUser where s.Mobile == mobile select s).FirstOrDefaultAsync();
                var iord = await (from s in _context.Request
                                  where s.WhoId == iuser.Id && s.Active == true
                                  orderby s.Id descending
                                  select s).Take(10).ToListAsync();
                foreach (var i in iord)
                {
                    //i.Who = "شماره رزرو: " + i.RequestCode + "<br>" + await getpack(i.PackId);
                }
                return Json(new { result = iord });
            }
            catch (Exception ex)
            {
                return Json(new { result = 1 });
            }
        }

        public async Task<string> getpack(int id)
        {
            var pack = await (from s in _context.Product
                                  //where s.Active == true && s.Deleted != true
                              where s.Id == id
                              select s).FirstOrDefaultAsync();
            if (pack != null)
            {
                //if (pack.Date == 0)
                //{
                //    return pack.Name + " - " + "NaN" + " " + getdype(pack.DateType) + "ه" + " - " + pack.Size + " " + Shared.getb(pack.SizeType);
                //}
                //else
                //{
                //    return pack.Name + " - " + (pack.Date) + " " + getdype(pack.DateType) + "ه" + " - " + (pack.Size) + " " + Shared.getb(pack.SizeType);
                //}
                return pack.Name;
            }
            else
            {
                return "NaN";
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> getOPlist()
        {
            string mobile = User.Identity.Name;
            var iuser = await (from s in _context.iUser where s.Mobile == mobile select s).FirstOrDefaultAsync();
            var iord = await (from s in _context.Request
                              where s.WhoId == iuser.Id && s.Active == true && s.Status == 1
                              orderby s.Id descending
                              select s).ToListAsync();

            foreach (var i in iord)
            {
                //i.Who = "شماره رزرو: " + i.RequestCode + "<br>" + await getpack(i.PackId);
            }
            return Json(new { result = iord });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Changrqstatus(int nowst, int who)
        {
            try
            {
                string mobile = User.Identity.Name;

                var iwhochang = await (from s in _context.Request
                                       where s.Id == who & s.Active == true
                                       select s).FirstOrDefaultAsync();

                if (nowst != iwhochang.Status)
                {
                    return Json(new { result = "رزرو ناموفق بود. وضعیت رزرو توسط ادمین تغییر کرده است" });
                }

                if (nowst == 0)
                {
                    nowst += 1;
                }
                else
                {
                    nowst -= 1;
                }
                iwhochang.Status = nowst;

                var bal = await (from s in _context.Balance
                                 where s.RequestId == who
                                 select s).FirstOrDefaultAsync();
                if (nowst == 1)
                {
                    bal.Active = true;
                }
                else
                {
                    bal.Active = false;
                }

                await _context.SaveChangesAsync();
                if (nowst == 1)
                {
                    return Json(new { result = "رزرو با موفقیت فعال شد" });
                }
                else
                {
                    return Json(new { result = "رزرو با موفقیت غیر فعال شد" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "مشکلی رخ داده است" + ex });
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GoToBank(string pamount, string pinfo)
        {
            try
            {
                if (string.IsNullOrEmpty(pamount))
                {
                    return Json(new { result = " لطفا مقادیر خواسته شده را وارد کنید" });

                }
                string mobile = User.Identity.Name;
                var iuser = await (from s in _context.iUser where s.Mobile == mobile select s).FirstOrDefaultAsync();

                int pamount2 = int.Parse(pamount.Replace(",", string.Empty));

                var nw = new Balance
                {
                    Active = false,
                    Consider = false,
                    Amount = pamount2,
                    Date = Shared.GetDate(),
                    Description = "افزایش موجودی توسط درگاه بانکی",
                    Time = Shared.GetTime(),
                    Who = "1",
                    WhoId = iuser.Id
                };
                await _context.AddAsync(nw);
                await _context.SaveChangesAsync();

                HttpContext.Session.SetString("pinfo", (string.IsNullOrEmpty(pinfo) ? "s" : pinfo));
                HttpContext.Session.SetString("pinfo2", nw.Id.ToString());

                var paylink1 = Url.Action(nameof(ZarinPalPayment), "iUser", new { Order = Shared.Encrypt(pamount2.ToString()), des = nw.Id + " - " + pinfo }, Request.Scheme);
                var paylink2 = Url.Action(nameof(MellatPayment), "iUser", new { Order = Shared.Encrypt(pamount2.ToString()), des = (100000 + nw.Id) }, Request.Scheme);

                return Json(new { result = 1, paylink1, paylink2 });
                //return Json(new { result = "متاسفانه پرداخت آنلاین در حال حاظر مقدور نیست" });

            }
            catch (Exception ex)
            {
                return Json(new { result = ex.ToString() });
            }
        }

        [HttpGet]
        public async Task<IActionResult> MellatPayment(string Order, string des, string mob)
        {
            return Content("خطا در اتصال");

            //var bpm = new Mellat("312", "213123", "312");
            //var response = await bpm.bpPayRequest(int.Parse(Shared.Decrypt(Order)), 100000 - int.Parse(des), "http://ronisim.ir/iUser/MellatPaymentVerify");

            //string[] StatusSendRequest = response.Body.@return.ToString().Split(',');

            //if (int.Parse(StatusSendRequest[0]) == 0)
            //{
            //    ViewBag.id = StatusSendRequest[1];
            //    return View();
            //    //return RedirectToAction("RedirectVPOS", "Home", new { id = StatusSendRequest[1] });
            //}

            //return Content(DesribtionStatusCode(int.Parse(response.Body.@return.ToString())));
        }

        public async Task<string> VerifyRequest(long saleOrderId, long saleReferenceId)
        {
            try
            {
                var bpm = new Mellat("3739058", "roni12345", "19079988");
                return (await bpm.bpVerifyRequest(saleOrderId, saleReferenceId)).Body.@return.ToString();
            }
            catch (Exception Error)
            {
                throw new Exception(Error.Message);
            }
        }

        public async Task<string> SettleRequest(long saleOrderId, long saleReferenceId)
        {
            try
            {
                var bpm = new Mellat("3739058", "roni12345", "19079988");
                return (await bpm.bpSettleRequest(saleOrderId, saleReferenceId)).Body.@return.ToString();
            }
            catch (Exception Error)
            {
                throw new Exception(Error.Message);
            }
        }

        public async Task<string> InquiryRequest(long saleOrderId, long saleReferenceId)
        {
            try
            {
                var bpm = new Mellat("3739058", "roni12345", "19079988");
                return (await bpm.bpInquiryRequest(saleOrderId, saleReferenceId)).Body.@return.ToString();
            }
            catch (Exception Error)
            {
                throw new Exception(Error.Message);
            }
        }

        public async Task<string> bpReversalRequest(long saleOrderId, long saleReferenceId)
        {
            try
            {
                var bpm = new Mellat("3739058", "roni12345", "19079988");
                return (await bpm.bpReversalRequest(saleOrderId, saleReferenceId)).Body.@return.ToString();
            }
            catch (Exception error)
            {
                throw new Exception(error.Message); ;
            }
        }

        [HttpPost]
        public async Task<IActionResult> MellatPaymentVerify(string ResCode, string ResId, long saleOrderId, long saleReferenceId)
        {
            try
            {
                string resultCode_bpinquiryRequest = "-9999";
                string resultCode_bpSettleRequest = "-9999";
                string resultCode_bpVerifyRequest = "-9999";

                if (ResCode != "0")
                {
                    var a1 = "فرایند پرداخت دراگاه بانکی موفقیت آمیز نبود";
                    HttpContext.Session.SetString("orderMes", a1);
                    HttpContext.Session.SetString("orderSuccess", "false");
                }
                else
                {
                    resultCode_bpVerifyRequest = await VerifyRequest(saleOrderId, saleReferenceId);
                    if (string.IsNullOrEmpty(resultCode_bpVerifyRequest))
                    {
                        #region Inquiry Request

                        resultCode_bpinquiryRequest = await InquiryRequest(saleOrderId, saleReferenceId);
                        if (int.Parse(resultCode_bpinquiryRequest) != 0)
                        {
                            var a1 = "فرایند پرداخت دراگاه بانکی موفقیت آمیز نبود. کد خطا: " + resultCode_bpinquiryRequest;
                            HttpContext.Session.SetString("orderMes", a1);
                            HttpContext.Session.SetString("orderSuccess", "false");
                        }

                        #endregion
                    }

                    if ((int.Parse(resultCode_bpVerifyRequest) == 0) || (int.Parse(resultCode_bpinquiryRequest) == 0))
                    {
                        #region SettleRequest

                        resultCode_bpSettleRequest = await SettleRequest(saleOrderId, saleReferenceId);
                        if ((int.Parse(resultCode_bpSettleRequest) == 0) || (int.Parse(resultCode_bpSettleRequest) == 45))
                        {
                            var nw = await (from s in _context.Balance where s.Id == Math.Abs(saleOrderId) select s).FirstOrDefaultAsync();
                            nw.Consider = true;
                            nw.Active = true;
                            nw.Description += " | کد پیگیری بانک ملت: " + saleReferenceId;
                            await _context.SaveChangesAsync();

                            var a1 = " مبلغ " + string.Format("{0:n0}", nw.Amount) + " ریال به حساب شما واریز گردید. بانک ملت - کد پرداخت " + saleReferenceId;
                            HttpContext.Session.SetString("orderMes", a1);
                            HttpContext.Session.SetString("orderSuccess", "true");
                        }
                        else
                        {
                            var a1 = "فرایند پرداخت دراگاه بانکی موفقیت آمیز نبود. تسویه کد خطا: " + resultCode_bpVerifyRequest + "-" + resultCode_bpinquiryRequest + "-" + resultCode_bpSettleRequest;
                        }

                        // Save information to Database...

                        #endregion
                    }
                    else
                    {
                        var a1 = "فرایند پرداخت دراگاه بانکی موفقیت آمیز نبود. کد خطا: " + resultCode_bpVerifyRequest + "-" + resultCode_bpinquiryRequest;
                        HttpContext.Session.SetString("orderMes", a1);
                        HttpContext.Session.SetString("orderSuccess", "false");
                    }
                }

                return RedirectToAction("Index", "iUser");


                //var ss = await bpm.bpVerifyRequest(saleOrderId,saleReferenceId);
                //return Content(ss.Body.@return.ToString());

            }
            catch (Exception ex)
            {
                return Content("<h1 style='text-align: center'>مشکلی پیش آمده است. لطفا با پشتیبانی در تماس باشید</h1>" + ex.ToString());
            }
        }


        public String DesribtionStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 0:
                    return "ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ";
                case 11:
                    return "ﺷﻤﺎره_ﻛﺎرت_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 12:
                    return "ﻣﻮﺟﻮدی_ﻛﺎﻓﻲ_ﻧﻴﺴﺖ";
                case 13:
                    return "رﻣﺰ_ﻧﺎدرﺳﺖ_اﺳﺖ";
                case 14:
                    return "ﺗﻌﺪاد_دﻓﻌﺎت_وارد_ﻛﺮدن_رﻣﺰ_ﺑﻴﺶ_از_ﺣﺪ_ﻣﺠﺎز_اﺳﺖ";
                case 15:
                    return "ﻛﺎرت_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 16:
                    return "دﻓﻌﺎت_ﺑﺮداﺷﺖ_وﺟﻪ_ﺑﻴﺶ_از_ﺣﺪ_ﻣﺠﺎز_اﺳﺖ";
                case 17:
                    return "ﻛﺎرﺑﺮ_از_اﻧﺠﺎم_ﺗﺮاﻛﻨﺶ_ﻣﻨﺼﺮف_ﺷﺪه_اﺳﺖ";
                case 18:
                    return "ﺗﺎرﻳﺦ_اﻧﻘﻀﺎی_ﻛﺎرت_ﮔﺬﺷﺘﻪ_اﺳﺖ";
                case 19:
                    return "ﻣﺒﻠﻎ_ﺑﺮداﺷﺖ_وﺟﻪ_ﺑﻴﺶ_از_ﺣﺪ_ﻣﺠﺎز_اﺳﺖ";
                case 111:
                    return "ﺻﺎدر_ﻛﻨﻨﺪه_ﻛﺎرت_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 112:
                    return "ﺧﻄﺎی_ﺳﻮﻳﻴﭻ_ﺻﺎدر_ﻛﻨﻨﺪه_ﻛﺎرت";
                case 113:
                    return "ﭘﺎﺳﺨﻲ_از_ﺻﺎدر_ﻛﻨﻨﺪه_ﻛﺎرت_درﻳﺎﻓﺖ_ﻧﺸﺪ";
                case 114:
                    return "دارﻧﺪه_ﻛﺎرت_ﻣﺠﺎز_ﺑﻪ_اﻧﺠﺎم_اﻳﻦ_ﺗﺮاﻛﻨﺶ_ﻧﻴﺴﺖ";
                case 21:
                    return "ﭘﺬﻳﺮﻧﺪه_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 23:
                    return "ﺧﻄﺎی_اﻣﻨﻴﺘﻲ_رخ_داده_اﺳﺖ";
                case 24:
                    return "اﻃﻼﻋﺎت_ﻛﺎرﺑﺮی_ﭘﺬﻳﺮﻧﺪه_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 25:
                    return "ﻣﺒﻠﻎ_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 31:
                    return "ﭘﺎﺳﺦ_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 32:
                    return "ﻓﺮﻣﺖ_اﻃﻼﻋﺎت_وارد_ﺷﺪه_ﺻﺤﻴﺢ_ﻧﻤﻲ_ﺑﺎﺷﺪ";
                case 33:
                    return "ﺣﺴﺎب_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 34:
                    return "ﺧﻄﺎی_ﺳﻴﺴﺘﻤﻲ";
                case 35:
                    return "ﺗﺎرﻳﺦ_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 41:
                    return "ﺷﻤﺎره_درﺧﻮاﺳﺖ_ﺗﻜﺮاری_اﺳﺖ";
                case 42:
                    return "ﺗﺮاﻛﻨﺶ_Sale_یافت_نشد_";
                case 43:
                    return "ﻗﺒﻼ_Verify_درﺧﻮاﺳﺖ_داده_ﺷﺪه_اﺳﺖ";



                case 44:
                    return "رزرو_verify_یافت_نشد";
                case 45:
                    return "ﺗﺮاﻛﻨﺶ_Settle_ﺷﺪه_اﺳﺖ";
                case 46:
                    return "ﺗﺮاﻛﻨﺶ_Settle_نشده_اﺳﺖ";

                case 47:
                    return "ﺗﺮاﻛﻨﺶ_Settle_یافت_نشد";
                case 48:
                    return "تراکنش_Reverse_شده_است";
                case 49:
                    return "تراکنش_Refund_یافت_نشد";
                case 412:
                    return "شناسه_قبض_نادرست_است";
                case 413:
                    return "ﺷﻨﺎﺳﻪ_ﭘﺮداﺧﺖ_ﻧﺎدرﺳﺖ_اﺳﺖ";
                case 414:
                    return "سازﻣﺎن_ﺻﺎدر_ﻛﻨﻨﺪه_ﻗﺒﺾ_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 415:
                    return "زﻣﺎن_ﺟﻠﺴﻪ_ﻛﺎری_ﺑﻪ_ﭘﺎﻳﺎن_رسیده_است";
                case 416:
                    return "ﺧﻄﺎ_در_ﺛﺒﺖ_اﻃﻼﻋﺎت";
                case 417:
                    return "ﺷﻨﺎﺳﻪ_ﭘﺮداﺧﺖ_ﻛﻨﻨﺪه_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 418:
                    return "اﺷﻜﺎل_در_ﺗﻌﺮﻳﻒ_اﻃﻼﻋﺎت_ﻣﺸﺘﺮی";
                case 419:
                    return "ﺗﻌﺪاد_دﻓﻌﺎت_ورود_اﻃﻼﻋﺎت_از_ﺣﺪ_ﻣﺠﺎز_ﮔﺬﺷﺘﻪ_اﺳﺖ";
                case 421:
                    return "IP_نامعتبر_است";

                case 51:
                    return "ﺗﺮاﻛﻨﺶ_ﺗﻜﺮاری_اﺳﺖ";
                case 54:
                    return "ﺗﺮاﻛﻨﺶ_ﻣﺮﺟﻊ_ﻣﻮﺟﻮد_ﻧﻴﺴﺖ";
                case 55:
                    return "ﺗﺮاﻛﻨﺶ_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 61:
                    return "ﺧﻄﺎ_در_واریز";

            }
            return "";
        }

        public class iPayment
        {
            public int Amount { get; set; }
            public string Description { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
        }

        public async Task<int> GetBalance(int id, string who)
        {
            var blnc = await (from s in _context.Balance
                              where s.Active == true && s.WhoId == id && s.Who == who
                              select s).ToListAsync();

            if (blnc == null)
            {
                return 0;
            }
            else
            {
                return blnc.Sum(c => c.Amount.Value);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ZarinPalPayment(string Order, string des)
        {
            var model = new iPayment
            {
                Amount = int.Parse(Shared.Decrypt(Order)) / 10,
                Description = " افزایش موجودی - کد پیگیری " + des,
                Email = "",
                Mobile = User.Identity.Name
            };

            //مرچنت کد خود را وارد کنید
            var payment = await new Zarinpal.Payment("90e4c7b0-c7a6-11e8-8703-005056a205be", model.Amount)
                .PaymentRequest(model.Description,
                    Url.Action(nameof(ZarinPalPaymentVerify), "iUser", new { Order }, Request.Scheme),
                    model.Email,
                    model.Mobile);
            //در صورت موفق آمیز بودن رزرو، کاربر به صفحه پرداخت هدایت می شود
            //در غیر این صورت خطا نمایش داده شود
            return BadRequest($"خطا در پرداخت. کد خطا:{payment.Status}");
        }


        public async Task<IActionResult> ZarinPalPaymentVerify(string Order, string Authority, string Status)
        {
            try
            {

                string mobile = User.Identity.Name;
                var iuser = await (from s in _context.iUser where s.Mobile == mobile select s).FirstOrDefaultAsync();


                var pinfo = HttpContext.Session.GetString("pinfo");
                var pinfo2 = HttpContext.Session.GetString("pinfo2");

                HttpContext.Session.Remove("pinfo");
                HttpContext.Session.Remove("pinfo2");


                if (Status == "NOK")
                {
                    var a1 = "فرایند پرداخت دراگاه بانکی موفقیت آمیز نبود";
                    HttpContext.Session.SetString("orderMes", a1);
                    HttpContext.Session.SetString("orderSuccess", "false");
                }
                else
                {
                    var zi = new Zarinpal.Payment("90e4c7b0-c7a6-11e8-8703-005056a205be", (int.Parse(Shared.Decrypt(Order)) / 10));
                    var verification = await zi.Verification(Authority);

                    var money = int.Parse(Shared.Decrypt(Order));

                    if (verification.Status == 100)
                    {

                        //ارسال کد تراکنش به جهت نمایش به کاربر
                        var refId = verification.RefId;

                        var nw = await (from s in _context.Balance where s.Id == int.Parse(pinfo2) select s).FirstOrDefaultAsync();
                        nw.Consider = true;
                        nw.Active = true;
                        nw.Description += " | کد پیگیری زرین پال: " + refId.ToString();
                        await _context.SaveChangesAsync();

                        var a1 = " مبلغ " + string.Format("{0:n0}", money) + " ریال به حساب شما واریز گردید. زرین پال - کد پرداخت " + refId.ToString();
                        HttpContext.Session.SetString("orderMes", a1);
                        HttpContext.Session.SetString("orderSuccess", "true");
                    }
                    else if (verification.Status == 101)
                    {
                        var a1 = " مبلغ " + string.Format("{0:n0}", money) + " ریال به حساب شما واریز گردید";
                        HttpContext.Session.SetString("orderMes", a1);
                        HttpContext.Session.SetString("orderSuccess", "true");
                    }
                    else
                    {
                        var a1 = "فرایند پرداخت دراگاه بانکی موفقیت آمیز نبود. کد خطا: " + verification.RefId + "--" + verification.Status;
                        HttpContext.Session.SetString("orderMes", a1);
                        HttpContext.Session.SetString("orderSuccess", "false");
                    }
                }

                return RedirectToAction("Index", "iUser");
                //return View("Index", PassToView);
            }
            catch (Exception ex)
            {
                return Content("<h1 style='text-align: center'>مشکلی پیش آمده است. لطفا با پشتیبانی در تماس باشید</h1>" + ex.ToString());
            }
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRequest(int pack, string fd, int ft, string td, int tt)
        {
            try
            {
                string mobile = User.Identity.Name;
                var iuser = await (from s in _context.iUser where s.Mobile == mobile select s).FirstOrDefaultAsync();
                var packk = await (from s in _context.Product where s.Id == pack select s).FirstOrDefaultAsync();

                var difd = (Shared.PersianTimeDiff2(fd, td));
                var dift = tt - ft;

                if (dift < 0 && difd < 0)
                {
                    return Json(new { result = "لطفا تاریخ و زمان را درست انتخاب کنید" });
                }

                var hm = difd * 24 + dift;
                var cost = packk.Price1 * hm;


                if (await GetBalance(iuser.Id, "1") < cost)
                {
                    return Json(new { result = "موجودی حساب شما کافی نمیباشد" });
                }

                var packk2 = await (from s in _context.Request
                                    where s.Id == pack &&
                                    ((Shared.PersianTimeDiff2(fd, s.FromDate) > 0 && (Shared.PersianTimeDiff2(s.ToDate, td) < 0) || (Shared.PersianTimeDiff2(fd, s.FromDate) == 0 && (Shared.PersianTimeDiff2(s.ToDate, td) == 0 && int.Parse(s.FromTime) > ft && int.Parse(s.ToTime) < tt))))
                                    select s).FirstOrDefaultAsync();

                if(packk != null)
                {
                    return Json(new { result = "این مکان در این تارخ و ساعت رزرو شده" });
                }
                var newone = new Request
                {
                    Active = true,
                    OrderDate = Shared.GetDate(),
                    OrderTime = Shared.GetTime(),
                    PackId = pack,
                    WhoId = iuser.Id,
                    Status = 1,
                    TotalPrice = cost,
                    FromDate = fd,
                    FromTime = ft.ToString(),
                    ToDate = td,
                    ToTime = tt.ToString()
                };

                await _context.AddAsync(newone);
                await _context.SaveChangesAsync();

                var nw = new Balance
                {
                    Active = true,
                    Who = "1",
                    WhoId = iuser.Id,
                    Amount = -cost,
                    RequestId = newone.Id,
                    Time = Shared.GetTime(),
                    Date = Shared.GetDate(),
                    Description = "رزرو شماره ",
                    Consider = true
                };

                await _context.AddAsync(nw);
                await _context.SaveChangesAsync();

                newone.RequestCode = (1000 + newone.Id).ToString();
                nw.Description = "رزرو شماره " + newone.RequestCode;

                await _context.SaveChangesAsync();
                //Shared.SendSMS(Shared.GetWhoSend(), "رونیسیم" + "\n" + "کاربر: " + iuser.Fname + " " + iuser.Lname + "\n" + "رزروی با شماره پیگیری: " + newone.RequestCode + " و " + "جای پارک " + await getpack(packk.Id) + " برای شماره موبایل " + pmobile + " به ثبت رسانده. لطفا پیگیری کنید");

                return Json(new { result = 1 });

            }
            catch (Exception ex)
            {
                return Json(new { result = "مشکلی پیش آمده است" + ex.ToString() });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel vm)
        {
            try
            {
                var cp = vm.cp;
                var np = vm.np;
                var renp = vm.renp;
                if (string.IsNullOrEmpty(cp) || string.IsNullOrEmpty(np) || string.IsNullOrEmpty(renp))
                {
                    return Json(new { result = 3 });
                }
                if (np != renp)
                {
                    return Json(new { result = 4 });
                }
                var mobile = User.Identity.Name;
                var studentToUpdate = await _context.iUser.SingleOrDefaultAsync(s => s.Mobile == mobile && s.PassWord == Shared.GetHash(cp + s.Salt));

                if (studentToUpdate != null)
                {
                    string Saltforpass = Shared.GetSalt();
                    studentToUpdate.Salt = Saltforpass;
                    studentToUpdate.PassWord = Shared.GetHash(np + Saltforpass);

                    await _context.SaveChangesAsync();

                    return Json(new { result = 1 });
                }
                else
                {
                    return Json(new { result = 2 });
                }
            }
            catch (Exception)
            {
                return Json(new { result = 0 });
            }
        }
    }
}