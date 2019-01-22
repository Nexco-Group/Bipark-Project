using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ronisim.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using OfficeOpenXml;
using SelectPdf;

namespace Ronisim.Controllers
{
    [Authorize]
    public class ManagementController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        static string rooturl = @"C:\\Website\\Contents";

        private readonly ronisimContext _context;
        public ManagementController(ronisimContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;

        }
        public async Task<IActionResult> Index()
        {
            string mobile = User.Identity.Name;

            var iUser = await (from s in _context.Admins
                               where s.Mobile == mobile
                               select s).FirstOrDefaultAsync();

            if (iUser == null)
            {
                return RedirectToAction("Index", "iUser");
            }

            //if (iUser.Verified != true)
            //{
            //    return RedirectToAction("Index", "MyProfile");
            //}
            var PassToView = new PassToAdminManagement()
            {
                iiUsers = await (from s in _context.iUser
                                 where s.Verify == true && s.Deleted != true && s.Active == true
                                 orderby s.Id descending
                                 select s).ToListAsync(),
                iActiveNews = await (from s in _context.News
                                     where s.Active == true
                                     orderby s.Id descending
                                     select s).ToListAsync(),
                iUnActiveNews = await (from s in _context.News
                                       where s.Active != true
                                       orderby s.Id descending
                                       select s).ToListAsync()
            };
            //PassToView.iTeachersRating = await getmyteachersrating(PassToView.iActiveClasses);

            return View(PassToView);
        }

        static bool IsImageFile(string f)
        {
            return f.EndsWith(".png", StringComparison.Ordinal) ||
                f.EndsWith(".jpg", StringComparison.Ordinal);
        }
        static string gettype(string f)
        {
            if (f.EndsWith(".png", StringComparison.Ordinal)) return ".png";
            else if (f.EndsWith(".jpg", StringComparison.Ordinal)) return ".jpg";
            else return "";
        }

        [HttpGet]
        public async Task<ActionResult> GetNotification(int fid)
        {
            var Chat1 = await (from s in _context.Request
                               where s.Status == 1 & s.Active == true
                               select s).ToListAsync();
            int count1 = (Chat1 != null && Chat1.Count > 0) ? Chat1.Count : 0;

            var Chat2 = await (from s in _context.iUser
                               where s.Verify != true && s.Active == true
                               select s).ToListAsync();

            int count2 = (Chat2 != null && Chat2.Count > 0) ? Chat2.Count : 0;

            return Json(new { count1, count2 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> getiUser(int who)
        {
            try
            {
                var rq = await (from s in _context.iUser
                                where s.Id == who && s.Deleted != true
                                select s).FirstOrDefaultAsync();

                return Json(new { result = 1, rq.Fname, rq.Lname, rq.Mobile, rq.Code });
            }
            catch (Exception)
            {
                return Json(new { result = "مشکلی پیش آمده" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStdInfo(EditInformationStd vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var iUser = await (from s in _context.iUser
                                       where s.Id == vm.Id
                                       select s).FirstOrDefaultAsync();

                    if (iUser == null)
                    {
                        return Json(new { result = "مشکلی پیش آمده است" });
                    }



                    iUser.Fname = vm.rfname;
                    iUser.Lname = vm.rlname;
                    iUser.Mobile = vm.rmobile;
                    if (vm.rcode != iUser.Code)
                    {
                        var sa = await (from s in _context.iUser
                                        where s.Code == vm.rcode
                                        select s).FirstOrDefaultAsync();
                        if (sa != null)
                        {
                            return Json(new { result = "کد وارد شده برای کاربر مجاز نمیباشد - برای کاربر دیگر اختصاص یافته" });
                        }
                        else
                        {
                            iUser.Code = vm.rcode;
                        }
                    }

                    bool ads = false;
                    if (!string.IsNullOrEmpty(vm.rpass) && vm.rpass != "undefined")
                    {
                        string Saltforpass = Shared.GetSalt();
                        iUser.Salt = Saltforpass;
                        iUser.PassWord = Shared.GetHash(vm.rpass + Saltforpass);
                        ads = true;
                    }

                    await _context.SaveChangesAsync();

                    if (ads)
                    {
                        return Json(new { result = "2" });
                    }
                    return Json(new { result = "1" });
                }
                else
                {
                    string Message = string.Join(" - ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                    return Json(new { result = Message });
                }
            }
            catch (Exception)
            {
                return Json(new { result = "مشکلی پیش آمده" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> getRSLS()
        {
            var rq = await (from s in _context.iUser
                            where s.Deleted != true
                            orderby s.Id descending
                            select s).ToListAsync();

            foreach (var r in rq)
            {
                r.Fname = r.Fname + " " + r.Lname;

            }

            return Json(new
            {
                result = rq
            });
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
                var studentToUpdate = await _context.Admins.SingleOrDefaultAsync(s => s.Mobile == mobile && s.PassWord == Shared.GetHash(cp + s.Salt));

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

        [HttpGet]
        public async Task<IActionResult> getRdet(int Oid)
        {
            var rs = await (from s in _context.iUser
                            where s.Id == Oid
                            select s).FirstOrDefaultAsync();

            return Json(new
            {
                a5 = await GetBalance(rs.Id, "1"),
                a6 = await (from s in _context.Request
                            where s.Active == true && s.WhoId == rs.Id && s.Status == 2
                            select s.OrderDate).FirstOrDefaultAsync()
            });
        }

        [HttpGet]
        public async Task<IActionResult> getAllmoney(string fromdate, string todate, int? status, int? whoid)
        {
            try
            {
                var jj = new List<Balance>();

                var who = "1";
                if (whoid != null && whoid == 0)
                {
                    who = "2";
                }

                if (string.IsNullOrEmpty(fromdate) && string.IsNullOrEmpty(todate))
                {
                    jj = await (from s in _context.Balance
                                where s.Active == true && s.Consider == true && (Shared.PersianTimeDiff2(s.Date, Shared.GetDate()) <= 1)
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
                                    where s.Active == true && s.Consider == true && (Shared.PersianTimeDiff2(s.Date, todate) >= 0)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                    orderby s.Id descending
                                    select s).ToListAsync();
                    }
                    else if (string.IsNullOrEmpty(todate))
                    {
                        jj = await (from s in _context.Balance
                                    where s.Active == true && s.Consider == true && (Shared.PersianTimeDiff2(fromdate, s.Date) >= 0)
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
                                    where s.Active == true && s.Consider == true && (Shared.PersianTimeDiff2(fromdate, s.Date) >= 0) && (Shared.PersianTimeDiff2(s.Date, todate) >= 0)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                    orderby s.Id descending
                                    select s).ToListAsync();
                    }
                }



                foreach (var j in jj)
                {
                    var a = j.Who;
                    j.Who = getname(j.WhoId, j.Who) + "|" + string.Format("{0:n0}", j.Amount);
                    j.WhoId = getcode(j.WhoId, a);
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
                var jj = new List<Balance>();

                var who = "1";
                if (whoid != null && whoid == 0)
                {
                    who = "2";
                }

                if (string.IsNullOrEmpty(fromdate) && string.IsNullOrEmpty(todate))
                {
                    jj = await (from s in _context.Balance
                                where s.Active == true && s.Consider == true && (Shared.PersianTimeDiff2(s.Date, Shared.GetDate()) <= 1)
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
                                    where s.Active == true && s.Consider == true && (Shared.PersianTimeDiff2(s.Date, todate) >= 0)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                    orderby s.Id descending
                                    select s).ToListAsync();
                    }
                    else if (string.IsNullOrEmpty(todate))
                    {
                        jj = await (from s in _context.Balance
                                    where s.Active == true && s.Consider == true && (Shared.PersianTimeDiff2(fromdate, s.Date) >= 0)
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
                                    where s.Active == true && s.Consider == true && (Shared.PersianTimeDiff2(fromdate, s.Date) >= 0) && (Shared.PersianTimeDiff2(s.Date, todate) >= 0)
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

                    var a = j.Who;
                    j.Who = getname(j.WhoId, j.Who) + "|" + string.Format("{0:n0}", j.Amount);
                    j.WhoId = getcode(j.WhoId, a);
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
                        worksheet.Cells[1, 2].Value = "نام فرد";
                        worksheet.Cells[1, 3].Value = "کد فرد";
                        worksheet.Cells[1, 4].Value = "مبلغ";
                        worksheet.Cells[1, 5].Value = "نوع تراکنش";
                        worksheet.Cells[1, 6].Value = "توضیح تراکنش";
                        worksheet.Cells[1, 7].Value = "تاریخ";
                        worksheet.Cells[1, 8].Value = "زمان";

                        // Inserts Data
                        for (int i = 0; i < jj.Count(); i++)
                        {
                            var obj = jj[i];
                            worksheet.Cells[i + 2, 1].Value = obj.Id;
                            worksheet.Cells[i + 2, 2].Value = obj.Who;
                            worksheet.Cells[i + 2, 3].Value = obj.WhoId;
                            worksheet.Cells[i + 2, 4].Value = obj.Amount;
                            if (obj.Amount > 0)
                            {
                                worksheet.Cells[i + 2, 5].Value = "واریز مبلغ - افزایش موجودی";
                            }
                            else
                            {
                                worksheet.Cells[i + 2, 5].Value = "کسر مبلغ - بابت رزرو";
                            }
                            worksheet.Cells[i + 2, 6].Value = obj.Description;
                            worksheet.Cells[i + 2, 7].Value = obj.Date;
                            worksheet.Cells[i + 2, 8].Value = obj.Time;
                        }
                        var ss = jj.Count() + 2;
                        worksheet.Cells[ss + 1, 4].Value = "ورودی";
                        worksheet.Cells[ss + 1, 5].Value = "خروجی";
                        worksheet.Cells[ss + 1, 6].Value = "جمع مبالغ";


                        worksheet.Cells[ss + 2, 4].Value = string.Format("{0:n0}", inc) + " ریال";
                        worksheet.Cells[ss + 2, 5].Value = string.Format("{0:n0}", outc) + " ریال";
                        worksheet.Cells[ss + 2, 6].Value = string.Format("{0:n0}", total) + " ریال";

                        // Format Header of Table
                        //using (ExcelRange rng = worksheet.Cells["A1:C1"])
                        //{
                        //    rng.Style.Font.Bold = true;
                        //    rng.Style.Fill.PatternType = ExcelFillStyle.Solid; //Set Pattern for the background to Solid 
                        //    //rng.Style.Fill.BackgroundColor.SetColor(Color.Gold); //Set color to DarkGray 
                        //    //rng.Style.Font.Color.SetColor(Color.Black);
                        //}


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
                var jj = new List<Balance>();

                var who = "1";
                if (whoid != null && whoid == 0)
                {
                    who = "2";
                }

                if (string.IsNullOrEmpty(fromdate) && string.IsNullOrEmpty(todate))
                {
                    jj = await (from s in _context.Balance
                                where s.Active == true && s.Consider == true && (Shared.PersianTimeDiff2(s.Date, Shared.GetDate()) <= 1)
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
                                    where s.Active == true && s.Consider == true && (Shared.PersianTimeDiff2(s.Date, todate) >= 0)
                                && (whoid == null ? true : (who == "2") ? s.Who == "2" : s.Who == who && s.WhoId == whoid)
                                && (status == null ? true : (status == 0) ? s.Amount >= 0 : s.Amount < 0)
                                    orderby s.Id descending
                                    select s).ToListAsync();
                    }
                    else if (string.IsNullOrEmpty(todate))
                    {
                        jj = await (from s in _context.Balance
                                    where s.Active == true && s.Consider == true && (Shared.PersianTimeDiff2(fromdate, s.Date) >= 0)
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
                                    where s.Active == true && s.Consider == true && (Shared.PersianTimeDiff2(fromdate, s.Date) >= 0) && (Shared.PersianTimeDiff2(s.Date, todate) >= 0)
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

                    var a = j.Who;
                    j.Who = getname(j.WhoId, j.Who) + "|" + string.Format("{0:n0}", j.Amount);
                    j.WhoId = getcode(j.WhoId, a);
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

                    string HtmlContent = Service.GetHTMLString2(jj);

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
        public async Task<IActionResult> getRmoney(int Oid)
        {
            try
            {
                var rs = await (from s in _context.iUser
                                where s.Id == Oid
                                select s).FirstOrDefaultAsync();

                var jj = await (from s in _context.Balance
                                where s.Active == true && s.Consider == true && s.WhoId == rs.Id && s.Who == "1"
                                orderby s.Id descending
                                select s).ToListAsync();


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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addbal(int inum, string ides, int who)
        {
            try
            {
                var newi = new Balance
                {
                    WhoId = who,
                    Amount = inum,
                    Description = "تغییر موجودی توسط ادمین | " + ides,
                    Date = Shared.GetDate(),
                    Time = Shared.GetTime(),
                    Active = true,
                    Consider = true,
                    Who = "1"
                };
                await _context.AddAsync(newi);
                await _context.SaveChangesAsync();

                return Json(new { result = "موجودی کاربر با موفقیت تغییر کرد" });
            }
            catch (Exception)
            {
                return Json(new { result = 0 });
            }
        }

        public async Task<int> GetBalance(int id, string who)
        {
            var blnc = await (from s in _context.Balance
                              where s.Active == true && s.Consider == true && s.WhoId == id && s.Who == who
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

        public Product getpackgig(int id)
        {
            var pack = (from s in _context.Product
                        where s.Id == id
                        select s).FirstOrDefault();

            return pack;
        }

        public async Task<string> getpack(int id)
        {
            var pack = await (from s in _context.Product
                              where s.Active == true && s.Deleted != true && s.Id == id
                              select s).FirstOrDefaultAsync();

            if (pack == null)
            {
                pack = await (from s in _context.Product
                              where s.Id == id
                              select s).FirstOrDefaultAsync();

                return "(جای پارک غیر فعال)" + pack.Name;

            }
            else
            {

                return pack.Name;

            }
        }

        [HttpGet]
        public async Task<IActionResult> getPRequest()
        {
            var rq = await (from s in _context.Request
                            where s.Status == 1 & s.Active == true
                            orderby s.Id descending
                            select s).ToListAsync();
            if (rq == null || rq.Count == 0)
            {
                return Json(new
                {
                    result = 0
                });
            }
            var res = new List<Request_plus>();
            foreach (var r in rq)
            {
                var nam = "";
                var cod = 10000001;

                var user = await (from s in _context.iUser
                                  where s.Id == r.WhoId
                                  select s).FirstOrDefaultAsync();
                nam = user.Fname + " " + user.Lname + " - کاربر";
                cod = user.Code.Value;


                res.Add(new Request_plus
                {
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

        [HttpGet]
        public async Task<IActionResult> getPRequestExcel()
        {
            var rq = await (from s in _context.Request
                            where s.Status == 1 & s.Active == true
                            orderby s.Id descending
                            select s).ToListAsync();
            if (rq == null || rq.Count == 0)
            {
                return Json(new
                {
                    result = 0
                });
            }

            float totalgig = 0;
            var totalmoney = 0;
            var totalmoney2 = 0;

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

                totalmoney += r.TotalPrice.Value;
                totalmoney2 += pack.Price2;


                res.Add(new Request_plus
                {
                    st = r.Status.Value,
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
                    note = r.Note                    
                });
            }

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
                // add a new worksheet to the empty workbook
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Request");
                //First add the headers
                worksheet.Cells[1, 1].Value = "شماره پیگیری رزرو";
                worksheet.Cells[1, 2].Value = "نام کاربر";
                worksheet.Cells[1, 3].Value = "کد کاربر";
                worksheet.Cells[1, 4].Value = "جای پارک رزروی";
                worksheet.Cells[1, 5].Value = "موبایل";
                worksheet.Cells[1, 6].Value = "قیمت";
                worksheet.Cells[1, 7].Value = "قیمت مصرف کننده";
                worksheet.Cells[1, 8].Value = "سود";
                worksheet.Cells[1, 9].Value = "حجم";
                worksheet.Cells[1, 10].Value = "توضیحات";
                worksheet.Cells[1, 11].Value = "اپراتور";
                worksheet.Cells[1, 12].Value = "تاریخ رزرو";
                worksheet.Cells[1, 13].Value = "وضعیت";

                // Inserts Data
                for (int i = 0; i < res.Count(); i++)
                {
                    var obj = res[i];
                    worksheet.Cells[i + 2, 1].Value = obj.RequestCode;
                    worksheet.Cells[i + 2, 2].Value = obj.Who;
                    worksheet.Cells[i + 2, 3].Value = obj.whoCode;
                    worksheet.Cells[i + 2, 4].Value = obj.Pack;
                    worksheet.Cells[i + 2, 5].Value = obj.ForMobile;
                    worksheet.Cells[i + 2, 6].Value = obj.money;
                    worksheet.Cells[i + 2, 7].Value = obj.money2;
                    worksheet.Cells[i + 2, 8].Value = obj.money2 - obj.money;
                    worksheet.Cells[i + 2, 9].Value = obj.gig;
                    worksheet.Cells[i + 2, 10].Value = obj.note;
                    worksheet.Cells[i + 2, 11].Value = obj.whoVerify;
                    worksheet.Cells[i + 2, 12].Value = obj.OrderDate;
                    worksheet.Cells[i + 2, 13].Value = obj.Status;
                }
                var ss = res.Count() + 2;
                worksheet.Cells[ss + 1, 9].Value = "جمع حجم";
                worksheet.Cells[ss + 1, 6].Value = "جمع مبالغ";
                worksheet.Cells[ss + 1, 7].Value = "جمع مبالغ - قیمت مصرف کننده";
                worksheet.Cells[ss + 1, 8].Value = "جمع سود";


                worksheet.Cells[ss + 2, 9].Value = totalgig + " گیگ";
                worksheet.Cells[ss + 2, 6].Value = totalmoney + " ریال";
                worksheet.Cells[ss + 2, 7].Value = totalmoney2 + " ریال";
                worksheet.Cells[ss + 2, 8].Value = (totalmoney2 - totalmoney) + " ریال";

                // Format Header of Table
                //using (ExcelRange rng = worksheet.Cells["A1:C1"])
                //{
                //    rng.Style.Font.Bold = true;
                //    rng.Style.Fill.PatternType = ExcelFillStyle.Solid; //Set Pattern for the background to Solid 
                //    //rng.Style.Fill.BackgroundColor.SetColor(Color.Gold); //Set color to DarkGray 
                //    //rng.Style.Font.Color.SetColor(Color.Black);
                //}


                package.Save(); //Save the workbook.
            }
            return Json(new
            {
                result = URL
            });
        }

        [HttpGet]
        public async Task<IActionResult> getPRequestPDF()
        {
            var rq = await (from s in _context.Request
                            where s.Status == 1 & s.Active == true
                            orderby s.Id descending
                            select s).ToListAsync();

            if (rq == null || rq.Count == 0)
            {
                return Json(new
                {
                    result = 0
                });
            }

            float totalgig = 0;
            var totalmoney = 0;
            var totalmoney2 = 0;

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

                totalmoney += r.TotalPrice.Value;
                totalmoney2 += pack.Price2;


                res.Add(new Request_plus
                {
                    st = r.Status.Value,
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

            string sFileName2 = @"PDF/AllRequest-" + Shared.GetDate().Replace('/', '-') + "-" + RandomString(8) + @".pdf";
            string sFileName = @"wwwroot/" + sFileName2;

            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName2);

            string HtmlContent = Service.GetHTMLString(res, totalmoney, totalmoney2, totalgig);

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


        public int? getcode(int? whoid, string who)
        {
            if (who == "1")
            {
                var user = (from s in _context.iUser
                            where s.Id == whoid
                            select s).FirstOrDefault();
                return user.Code;
            }
            else
            {
                return 10000001;
            }
        }

        public string getname(int? whoid, string who)
        {
            if (who == "1")
            {
                var user = (from s in _context.iUser
                            where s.Id == whoid
                            select s).FirstOrDefault();
                return user.Fname + " " + user.Lname + " - کاربر";
            }
            else
            {
                return "کاربر صفحه اول";
            }
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
        public partial class Request_plus
        {
            public int st { get; set; }
            public int Id { get; set; }
            public int money { get; set; }
            public int money2 { get; set; }
            public string FromDate { get; set; }
            public string FromTime { get; set; }
            public string ToDate { get; set; }
            public string ToTime { get; set; }
            public float gig { get; set; }
            public string RequestCode { get; set; }
            public string Who { get; set; }
            public int? whoCode { get; set; }
            public string ForMobile { get; set; }
            public string OrderDate { get; set; }
            public string Pack { get; set; }
            public string Status { get; set; }
            public string note { get; set; }
            public bool? Active { get; set; }
            public string whoVerify { get; set; }
        }



        [HttpGet]
        public async Task<IActionResult> getRequest(string fromdate, string todate, string formobile, int? status, int? whoid, int? size)
        {
            var rq = new List<Request>();
            var who = "1";
            if (whoid != null && whoid == 0)
            {
                who = "2";
            }
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

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpGet]
        public async Task<IActionResult> getProduct()
        {
            var rq = await (from s in _context.Product
                            where s.Deleted != true
                            orderby s.Id descending
                            select s).ToListAsync();

            return Json(new
            {
                result = rq
            });
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(Product vm)
        {
            try
            {
                if (string.IsNullOrEmpty(vm.Name))
                {
                    return Json(new { result = "مقادیر ورودی را کنترل کنید" });
                }
                vm.Active = true;
                vm.CreatDate = Shared.GetDate();

                await _context.Product.AddAsync(vm);

                await _context.SaveChangesAsync();
                return Json(new { result = 1 });

            }
            catch (Exception)
            {
                return Json(new { result = "مقادیر ورودی را کنترل کنید" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Changrslstatus(bool nowst, int who)
        {
            try
            {
                bool impst = !nowst;
                string mobile = User.Identity.Name;

                var iwhochang = await (from s in _context.iUser
                                       where s.Id == who
                                       select s).FirstOrDefaultAsync();
                iwhochang.Active = impst;

                await _context.SaveChangesAsync();
                if (impst)
                {
                    Shared.SendSMS(iwhochang.Mobile, "رونیسیم" + "\n" + "کاربر محترم حساب کاربری شما در رونیسیم فعال شده است");
                    return Json(new { result = "کاربر با موفقیت فعال شد" });
                }
                else
                {
                    Shared.SendSMS(iwhochang.Mobile, "رونیسیم" + "\n" + "کاربر محترم حساب کاربری شما در رونیسیم غیر فعال شده است");

                    return Json(new { result = "کاربر با موفقیت غیر فعال شد" });
                }
            }
            catch (Exception)
            {
                return Json(new { result = "مشکلی رخ داده است" });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> verifyrsl(int who)
        {
            try
            {
                var iwhochang = await (from s in _context.iUser
                                       where s.Id == who
                                       select s).FirstOrDefaultAsync();
                iwhochang.Verify = true;

                await _context.SaveChangesAsync();

                Shared.SendSMS(iwhochang.Mobile, "رونیسیم" + "\n" + "کاربر محترم حساب کاربری شما توسط ادمین تایید شده است");

                return Json(new { result = "کاربر با موفقیت تایید شد" });
            }
            catch (Exception)
            {
                return Json(new { result = "مشکلی رخ داده است" });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> deleter(int who)
        {
            try
            {
                var iUser = await (from s in _context.iUser
                                   where s.Id == who
                                   select s).FirstOrDefaultAsync();

                iUser.Deleted = true;
                iUser.Active = false;
                //var ss = await (from s in _context.Request
                //                where s.WhoId == iUser.Id
                //                select s).ToListAsync();
                //foreach (var a in ss)
                //{
                //    a.Deleted = true;
                //}
                Shared.SendSMS(iUser.Mobile, "رونیسیم" + "\n" + "کاربر محترم حساب کاربری شما در رونیسیم غیر فعال شده است");

                await _context.SaveChangesAsync();
                return Json(new { result = "کاربر با موفقیت حذف شد" });
            }
            catch (Exception ex)
            {
                return Json(new { result = "مشکلی رخ داده است" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> deletep(int who)
        {
            try
            {
                string mobile = User.Identity.Name;

                var iUser = await (from s in _context.Product
                                   where s.Id == who
                                   select s).FirstOrDefaultAsync();

                iUser.Deleted = true;
                iUser.Active = false;

                //var ss = await (from s in _context.Request
                //                where s.PackId == iUser.Id
                //                select s).ToListAsync();
                //foreach (var a in ss)
                //{
                //    a.Deleted = true;
                //}

                await _context.SaveChangesAsync();

                return Json(new { result = "جای پارک با موفقیت حذف شد" });

            }
            catch (Exception ex)
            {
                return Json(new { result = "مشکلی رخ داده است" + ex });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Changepstatus(bool nowst, int who)
        {
            try
            {
                bool impst = !nowst;
                string mobile = User.Identity.Name;

                var iUser = await (from s in _context.Product
                                   where s.Id == who
                                   select s).FirstOrDefaultAsync();

                iUser.Active = impst;

                await _context.SaveChangesAsync();
                if (impst)
                {
                    return Json(new { result = "جای پارک با موفقیت فعال شد" });
                }
                else
                {
                    return Json(new { result = "جای پارک با موفقیت غیر فعال شد" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "مشکلی رخ داده است" + ex });
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Changrqstatus(int nowst, int who, string note, string vrf)
        {
            try
            {
                bool impst = nowst == 1;
                string mobile = User.Identity.Name;

                var iwhochang = await (from s in _context.Request
                                       where s.Id == who
                                       select s).FirstOrDefaultAsync();

                iwhochang.Note = note;

                if (impst)
                {
                    iwhochang.Status = 2;
                }
                else
                {
                    iwhochang.Status = 4;
                }

                var bal = await (from s in _context.Balance
                                 where s.RequestId == who
                                 select s).FirstOrDefaultAsync();
                if (impst)
                {
                    bal.Active = true;
                    bal.Consider = true;

                }
                else
                {
                    bal.Active = false;
                    bal.Consider = false;
                }

                await _context.SaveChangesAsync();

                if (impst)
                {
                    var mes = "جای پارک " + await getpack(iwhochang.PackId) + " با کد رهگیری " + iwhochang.RequestCode + "برای شما فعال شد " + "\n" + "با استعلام کد " + "*400*2*2*1#" + " میتوانید مانده اعتبار بگیرید" + "\n" + "تماس با پشتیبانی: 09354978000";
                    return Json(new { result = "رزرو با تایید فعال شد" });
                }
                else
                {
                    return Json(new { result = "رزرو با موفقیت لغو شد" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = "مشکلی رخ داده است" + ex });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNews(AddNews vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (vm.Newsimage != null)
                    {
                        if (vm.Newsimage.Length <= 350000)
                        {
                            if (IsImageFile(vm.Newsimage.FileName.ToLower()))
                            {
                                var uploads = Path.Combine(rooturl, "NewsImage");

                                string filename = "News-" + Shared.GetSMSCode() + "-" + Shared.GetSalt() + gettype(vm.Newsimage.FileName.ToString().ToLower());
                                using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                                {
                                    await vm.Newsimage.CopyToAsync(fileStream);
                                }

                                var newone = new News()
                                {
                                    Text = vm.Newstitle,
                                    Link = vm.Newsurl,
                                    iUser = vm.iUserchecked,
                                    Ruser = vm.ruserchecked,
                                    Image = filename,
                                    Date = Shared.GetDate(),
                                    Active = true
                                };
                                await _context.AddAsync(newone);
                                await _context.SaveChangesAsync();
                                return Json(new { result = 1 });
                            }
                            else
                            {
                                return Json(new { result = " jpg , png  فرمت فایل ورودی مجاز نیست فقط فایل با فرمت " });
                            }
                        }
                        else
                        {
                            return Json(new { result = "حجم فایل ورودی بیشتر از حد مجاز (300 کیلوبایت) می باشد" });
                        }
                    }
                    else
                    {
                        var newone = new News()
                        {
                            Text = vm.Newstitle,
                            Link = vm.Newsurl,
                            Image = "",
                            iUser = vm.iUserchecked,
                            Ruser = vm.ruserchecked,
                            Date = Shared.GetDate(),
                            Active = true
                        };
                        await _context.AddAsync(newone);
                        await _context.SaveChangesAsync();
                        return Json(new { result = 1 });
                    }
                }
                else
                {
                    var Message = string.Join(" - ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
                    return Json(new { result = Message });
                }
            }
            catch (Exception)
            {
                return Json(new { result = "ثبت اطلاعیه ناموفق بود" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditNews(EditNews vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var iNews = await (from s in _context.News
                                       where s.Id == vm.Id
                                       select s).FirstOrDefaultAsync();

                    iNews.Text = vm.Newstitle;
                    iNews.iUser = vm.schoolchecked;
                    iNews.Ruser = vm.teacherchecked;

                    await _context.SaveChangesAsync();
                    return Json(new { result = 1 });
                }
                else
                {
                    var Message = string.Join(" - ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
                    return Json(new { result = Message });
                }
            }
            catch (Exception)
            {
                return Json(new { result = "ادیت خبر ناموفق بود" });
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Changnewsstatus(bool nowst, int who)
        {
            try
            {
                bool impst = !nowst;
                string mobile = User.Identity.Name;

                var iUser = await (from s in _context.News
                                   where s.Id == who
                                   select s).FirstOrDefaultAsync();

                iUser.Active = impst;

                await _context.SaveChangesAsync();
                if (impst)
                {
                    return Json(new { result = "اطلاعیه با موفقیت فعال شد" });
                }
                else
                {
                    return Json(new { result = "اطلاعیه با موفقیت غیر فعال شد - توجه: بعد از رفرش صفحه تغییرات قابل مشاهده است" });
                }
            }
            catch (Exception)
            {
                return Json(new { result = "مشکلی رخ داده است" });
            }
        }

    }
}