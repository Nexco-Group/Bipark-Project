using Microsoft.AspNetCore.Mvc;
using Ronisim.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using BankMellat;

namespace Ronisim.Controllers
{
    public class HomeController : Controller
    {
        private readonly ronisimContext _context;
        //static string rooturl = @"C:\\Website\\Contents";
        public HomeController(ronisimContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                PassToOrders PassToView = new PassToOrders
                {
                    iNews = await (from s in _context.News
                                   where s.Active == true && s.Ruser == true
                                   orderby s.Id descending
                                   select s).ToListAsync()
                };

                //var sa = await (from s in _context.Product
                //                where s.Active == true && s.Deleted != true
                //                orderby s.Id descending
                //                select s).GroupBy(a => new { a.DateType, a.Date }).ToListAsync();
                //var repacktime = new List<packtime>();

                //foreach (var u in sa)
                //{
                //    repacktime.Add(new packtime { type = u.Key.DateType, date = u.Key.Date, name = (u.Key.Date) + " " + getdype(u.Key.DateType) + "ه" });
                //}

                //PassToView.packtime = repacktime;


                //var rq = await (from s in _context.Product
                //                where s.Active == true && s.Deleted != true
                //                orderby s.Size descending, s.SizeType descending, s.Date descending, s.DateType descending
                //                select s).ToListAsync();

                //foreach (var r in rq)
                //{
                //    r.Category = await getpack(r.Id);
                //}
                //PassToView.iProduct = rq;


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

                return View("Index", PassToView);

            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        //public async Task<string> getpack(int id)
        //{
        //    var pack = await (from s in _context.Product
        //                          //where s.Active == true && s.Deleted != true
        //                      where s.Id == id
        //                      select s).FirstOrDefaultAsync();
        //    if (pack != null)
        //    {
        //        if (pack.Date == 0)
        //        {
        //            return pack.Name + " - " + "NaN" + " " + getdype(pack.DateType) + "ه" + " - " + (pack.Size) + " " + Shared.getb(pack.SizeType);
        //        }
        //        else
        //        {
        //            return pack.Name + " - " + (pack.Date) + " " + getdype(pack.DateType) + "ه" + " - " + (pack.Size) + " " + Shared.getb(pack.SizeType);
        //        }
        //    }
        //    else
        //    {
        //        return "NaN";
        //    }
        //}
        



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
    }
}