using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ronisim.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Ronisim.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly ronisimContext _context;
        public AdminController(ronisimContext context)
        {
            _context = context;
        }
        //static string rooturl = @"C:\\Website\\Contents";

        public async Task<IActionResult> Index()
        {
            string mobile = User.Identity.Name;

            Admins iUser = await (from s in _context.Admins
                                  where s.Mobile == mobile
                                  select s).FirstOrDefaultAsync();

            if (iUser == null)
            {
                return RedirectToAction("Index", "Logout");
            }

            var mycode = 10;

            if (iUser.AccessLevel == "1")
            {
                mycode = 0;
            }

            var PassToView = new PassToMyChats()
            {
                iUser = iUser
            };
            return View(PassToView);
        }

        [HttpGet]
        public async Task<IActionResult> getstoreitem()
        {
            try
            {
                var Items = await _context.Product
                                    .ToListAsync();
                //foreach (var item in Items)
                //{
                //    var a = await (from s in _context.Store_Stock
                //                   where s.Active == true && s.Item_Id == item.Id
                //                   select s).ToListAsync();
                //    item.Stock = a.Sum(i => i.Number);
                //}
                return Json(new { result = Items });
            }
            catch (Exception ex)
            {
                return Json(new { result = ex.ToString() });
            }
        }
        //[HttpGet]
        //public async Task<IActionResult> getitemlog()
        //{
        //    try
        //    {
        //        var a = await (from s in _context.Store_Stock
        //                       orderby s.Id descending
        //                       select s).ToListAsync();

        //        var itm = new List<Store_Stock2>();
        //        foreach (var e in a)
        //        {
        //            var t = JsonConvert.DeserializeObject<Store_Stock2>(JsonConvert.SerializeObject(e));
        //            t.Item = await (from s in _context.Store_Item
        //                            where s.Id == t.Item_Id
        //                            select s.Title).FirstOrDefaultAsync();
        //            itm.Add(t);
        //        }

        //        return Json(new { result = itm });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = 0 });
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult genosessid(int sch)
        //{
        //    try
        //    {
        //        var mobile = (from s in _context.Schools
        //                      where s.Id == sch
        //                      select s.Mobile).FirstOrDefault();

        //        HttpContext.Session.SetString("OFSlMobile", mobile);
        //        return Json(new { result = 1 });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = 0 });
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult genosessid2(int order)
        //{
        //    try
        //    {
        //        HttpContext.Session.SetString("OFSlOrder", order.ToString());

        //        return Json(new { result = 1 });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = 0 });
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> StockItem(int inum, string ides, int who)
        //{
        //    try
        //    {
        //        var newi = new Store_Stock
        //        {
        //            Item_Id = who,
        //            Number = inum,
        //            Description = ides,
        //            Date = Shared.GetDate(),
        //            Time = Shared.GetTime(),
        //            Active = true
        //        };
        //        await _context.AddAsync(newi);
        //        await _context.SaveChangesAsync();

        //        return Json(new { result = "Item stock successfully updated" });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = "مشکلی رخ داده است" });
        //    }
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CAItem(bool nowst, int who)
        {
            try
            {
                bool impst = !nowst;

                var iwhochang = await (from s in _context.Product
                                       where s.Id == who
                                       select s).FirstOrDefaultAsync();
                iwhochang.Active = impst;

                await _context.SaveChangesAsync();
                if (impst)
                {
                    return Json(new { result = "Item has been activated" });
                }
                else
                {
                    return Json(new { result = "Item has been inactivated" });
                }
            }
            catch (Exception)
            {
                return Json(new { result = "مشکلی رخ داده است" });
            }
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CAStockLog(bool nowst, int who)
        //{
        //    try
        //    {
        //        bool impst = !nowst;

        //        var iwhochang = await (from s in _context.Store_Stock
        //                               where s.Id == who
        //                               select s).FirstOrDefaultAsync();
        //        iwhochang.Active = impst;

        //        await _context.SaveChangesAsync();
        //        if (impst)
        //        {
        //            return Json(new { result = "Log has been activated" });
        //        }
        //        else
        //        {
        //            return Json(new { result = "Log has been inactivated" });
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = "مشکلی رخ داده است" });
        //    }
        //}

        [HttpGet]
        public async Task<IActionResult> getorders()
        {
            try
            {
                var iwhochang = await (from s in _context.Request
                                       where s.Status > 1
                                       orderby s.Id descending
                                       select s).ToListAsync();
                List<string> names = new List<string>();
                foreach (var i in iwhochang)
                {
                    names.Add(await (from s in _context.Reseller
                                     where s.Id == i.WhoId
                                     select s.Fname + " " + s.Lname).FirstOrDefaultAsync());
                }

                return Json(new { result = iwhochang, names });
            }
            catch (Exception)
            {
                return Json(new { result = 0 });
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> vBT(string mes, int who)
        //{
        //    try
        //    {
        //        var or = await (from s in _context.Store_Orders
        //                        where s.Id == who && s.Active == true
        //                        select s).LastOrDefaultAsync();

        //        var op = await (from s in _context.Store_OrderPayment
        //                        where s.Order_Id == who && s.Active == true
        //                        select s).LastOrDefaultAsync();
        //        var np = new Store_Balance
        //        {
        //            Active = true,
        //            Amount = op.Money,
        //            Date = Shared.GetDate(),
        //            Time = Shared.GetTime(),
        //            Description = "Counted in order" + who + " Payment" + op.Id,
        //            Order_Id = who,
        //            School_Id = or.School_Id,
        //            Consider = false
        //        };

        //        await _context.AddAsync(np);
        //        await _context.SaveChangesAsync();

        //        op.BTBL_Id = np.Id;

        //        await _context.SaveChangesAsync();

        //        return Json(new { result = "Order transaction confirmed!" });

        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = "مشکلی رخ داده است" });
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> vorder(string mes, int who)
        //{
        //    try
        //    {
        //        var iwhochang = await (from s in _context.Store_Orders
        //                               where s.Id == who
        //                               select s).FirstOrDefaultAsync();

        //        var res = await CostStockOfCart(who);

        //        if (res != "yes")
        //        {
        //            return Json(new { result = "مشکلی رخ داده است" });
        //        }

        //        var extra = "";

        //        var op = await (from s in _context.Store_OrderPayment
        //                        where s.Active == true && s.Order_Id == who
        //                        select s).LastOrDefaultAsync();

        //        if (op.BTBL_Id == null)
        //        {
        //            await _context.AddAsync(new Store_Balance
        //            {
        //                Active = true,
        //                Amount = op.Money,
        //                Consider = true,
        //                Date = Shared.GetDate(),
        //                Time = Shared.GetTime(),
        //                Description = "Counted in order" + who + " Payment" + op.Id,
        //                Order_Id = who,
        //                School_Id = iwhochang.School_Id
        //            });
        //        }
        //        else
        //        {
        //            var BTBL = await (from s in _context.Store_Balance
        //                              where s.Id == op.BTBL_Id
        //                              select s).LastOrDefaultAsync();
        //            if (BTBL == null)
        //            {
        //                await _context.AddAsync(new Store_Balance
        //                {
        //                    Active = true,
        //                    Amount = op.Money,
        //                    Consider = true,
        //                    Date = Shared.GetDate(),
        //                    Time = Shared.GetTime(),
        //                    Description = "Counted in order" + who + " Payment" + op.Id,
        //                    Order_Id = who,
        //                    School_Id = iwhochang.School_Id
        //                });
        //            }
        //            else
        //            {
        //                if (BTBL.Active != true)
        //                {
        //                    BTBL.Active = true;
        //                }
        //                if (BTBL.Consider == false)
        //                {
        //                    BTBL.Consider = true;
        //                }
        //            }
        //        }



        //        iwhochang.OrderStep = 3;

        //        await _context.SaveChangesAsync();

        //        new Service(_context).SendSMS(iwhochang.Mobile, mes);


        //        return Json(new { result = "Order has been verified. And SMS has been sent" + extra });

        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = "مشکلی رخ داده است" });
        //    }
        //}

        //public async Task<string> CostStockOfCart(int order_id)
        //{
        //    try
        //    {
        //        var iwhochang = await (from s in _context.Store_Cart
        //                               where s.Order_Id == order_id && s.Active == true
        //                               select s).ToListAsync();
        //        foreach (var item in iwhochang)
        //        {
        //            var a = new Store_Stock
        //            {
        //                Active = true,
        //                Date = Shared.GetDate(),
        //                Item_Id = item.Item_Id,
        //                Number = -item.Count,
        //                Order_Id = order_id,
        //                Time = Shared.GetTime(),
        //                Description = "Counted in order: " + order_id + " cart:" + item.Id
        //            };
        //            await _context.AddAsync(a);
        //        }


        //        await _context.SaveChangesAsync();

        //        return "yes";

        //    }
        //    catch (Exception)
        //    {
        //        return "مشکلی رخ داده است";
        //    }
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> vship(string mes, int who)
        //{
        //    try
        //    {
        //        var iwhochang = await (from s in _context.Store_Orders
        //                               where s.Id == who
        //                               select s).FirstOrDefaultAsync();
        //        iwhochang.OrderStep = 4;

        //        await _context.SaveChangesAsync();

        //        new Service(_context).SendSMS(iwhochang.Mobile, mes);

        //        return Json(new { result = "Order has been Shipped. And SMS has been sent" });

        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = "مشکلی رخ داده است" });
        //    }
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CAOrder(bool nowst, int who)
        {
            try
            {
                bool impst = !nowst;

                var iwhochang = await (from s in _context.Request
                                       where s.Id == who
                                       select s).FirstOrDefaultAsync();
                iwhochang.Active = impst;

                await _context.SaveChangesAsync();
                if (impst)
                {
                    return Json(new { result = "Order has been activated" });
                }
                else
                {
                    return Json(new { result = "Order has been inactivated" });
                }
            }
            catch (Exception)
            {
                return Json(new { result = "مشکلی رخ داده است" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> getPayment()
        {
            try
            {
                var iwhochang = await (from s in _context.Balance
                                       orderby s.Id descending
                                       select s).ToListAsync();

                return Json(new { result = iwhochang });
            }
            catch (Exception)
            {
                return Json(new { result = 0 });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CAPayment(bool nowst, int who)
        {
            try
            {
                bool impst = !nowst;

                var iwhochang = await (from s in _context.Balance
                                       where s.Id == who
                                       select s).FirstOrDefaultAsync();
                iwhochang.Active = impst;

                await _context.SaveChangesAsync();
                if (impst)
                {
                    return Json(new { result = "Payment Record has been activated" });
                }
                else
                {
                    return Json(new { result = "Payment Record has been inactivated" });
                }
            }
            catch (Exception)
            {
                return Json(new { result = "مشکلی رخ داده است" });
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> getSchool()
        //{
        //    try
        //    {
        //        var sch = await (from s in _context.Schools
        //                         where s.Active == true
        //                         orderby s.Id descending
        //                         select s).ToListAsync();
        //        for (var i = 0; i < sch.Count; i++)
        //        {
        //            sch[i] = await oksch(sch[i]);
        //        }
        //        return Json(new { result = sch });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = 0 });
        //    }
        //}

        //public async Task<Schools> oksch(Schools i)
        //{
        //    try
        //    {
        //        var blnc = await (from s in _context.Store_Balance
        //                          where s.Active == true && s.School_Id == i.Id
        //                          select s).ToListAsync();

        //        var a = new Service(_context).getcity(i.City_Id);
        //        i.FnameEn = a.province;
        //        i.LnameEn = a.city;

        //        if (blnc == null)
        //        {
        //            i.Balance = 0;
        //        }
        //        else
        //        {
        //            i.Balance = blnc.Sum(c => c.Amount);
        //        }
        //        return i;
        //    }
        //    catch (Exception)
        //    {
        //        return i;
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> addbal(int inum, string ides, int who)
        //{
        //    try
        //    {
        //        var newi = new Store_Balance
        //        {
        //            School_Id = who,
        //            Amount = inum,
        //            Description = ides,
        //            Date = Shared.GetDate(),
        //            Time = Shared.GetTime(),
        //            Active = true
        //        };
        //        await _context.AddAsync(newi);
        //        await _context.SaveChangesAsync();

        //        return Json(new { result = "Balance successfully updated" });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = 0 });
        //    }
        //}

        public partial class nes2
        {
            public string name { get; set; }
            public int count { get; set; }
            public int total { get; set; }
            public int price { get; set; }
            public int discount { get; set; }
        }
        public partial class nes31
        {
            public int orderTotal { get; set; }
            public int? discount { get; set; }
            public int shippingCost { get; set; }
            public int shippingType { get; set; }
            public string paymentType { get; set; }
            public int balanceWithdrawal { get; set; }
            public int totalp { get; set; }

            public int? dargahP { get; set; }
            public string gate { get; set; }
            public string description { get; set; }
            public string authority { get; set; }
            public string refId { get; set; }
            public bool? success { get; set; }

        }


        //[HttpGet]
        //public async Task<IActionResult> getPaymentdet(int Oid)
        //{
        //    try
        //    {
        //        var order = await (from s in _context.Store_Orders
        //                           where s.Active == true && s.Id == Oid
        //                           select s).LastOrDefaultAsync();

        //        var op = await (from s in _context.Store_OrderPayment
        //                        where s.Active == true && s.Order_Id == Oid
        //                        select s).LastOrDefaultAsync();
        //        if (order == null || op == null)
        //        {
        //            return Json(new { result = 0 });
        //        }

        //        var ret = new nes31
        //        {
        //            totalp = op.Money,
        //            discount = order.Discount,
        //            orderTotal = order.TotalPrice,
        //            shippingCost = 0,
        //            shippingType = 0
        //        };

        //        if (op.Type == 1)
        //        {
        //            ret.paymentType = "Online Payment";
        //            if (op.Balance_Log_Id != null)
        //            {
        //                var bl = await (from s in _context.Store_Balance
        //                                where s.Active == true && s.Id == op.Balance_Log_Id
        //                                select s).LastOrDefaultAsync();
        //                if (bl != null)
        //                {
        //                    ret.balanceWithdrawal = bl.Amount;
        //                }
        //            }
        //            var online = await (from s in _context.Store_Payment_Online
        //                                where s.Payment_Id == op.Id
        //                                select s).LastOrDefaultAsync();
        //            if (online != null)
        //            {
        //                ret.dargahP = online.Amount;
        //                ret.gate = online.Gate;
        //                ret.description = online.Description;
        //                ret.authority = online.Authority;
        //                ret.refId = online.RefId;
        //                ret.success = online.Success;
        //            }
        //        }
        //        else
        //        {
        //            ret.paymentType = "Bank Transfer";
        //            if (op.BTBL_Id == null)
        //            {
        //                ret.success = false;
        //            }
        //            else
        //            {
        //                var BTBL = await (from s in _context.Store_Balance
        //                                  where s.Id == op.BTBL_Id && s.Active == true
        //                                  select s).LastOrDefaultAsync();
        //                if (BTBL != null)
        //                {
        //                    ret.success = true;
        //                    ret.description = BTBL.Description;
        //                }
        //                else
        //                {
        //                    ret.success = false;
        //                }
        //            }
        //        }

        //        return Json(new { result = ret });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = 0 });
        //    }
        //}


        //public async Task<string> getPaymentinfo(int Oid)
        //{
        //    try
        //    {
        //        var order = await (from s in _context.Store_Orders
        //                           where s.Active == true && s.Id == Oid
        //                           select s).LastOrDefaultAsync();

        //        var op = await (from s in _context.Store_OrderPayment
        //                        where s.Active == true && s.Order_Id == Oid
        //                        select s).LastOrDefaultAsync();
        //        if (order == null || op == null)
        //        {
        //            return "";
        //        }


        //        var ret = new nes31
        //        {
        //            totalp = op.Money,
        //            discount = order.Discount,
        //            orderTotal = order.TotalPrice,
        //            shippingCost = 0,
        //            shippingType = 0
        //        };

        //        if (op.Balance_Log_Id != null)
        //        {
        //            var bl = await (from s in _context.Store_Balance
        //                            where s.Active == true && s.Id == op.Balance_Log_Id
        //                            select s).LastOrDefaultAsync();
        //            if (bl != null)
        //            {
        //                ret.balanceWithdrawal = bl.Amount;
        //            }
        //        }
        //        var online = await (from s in _context.Store_Payment_Online
        //                            where s.Payment_Id == op.Id
        //                            select s).LastOrDefaultAsync();
        //        if (online != null)
        //        {
        //            ret.dargahP = online.Amount;
        //            ret.gate = online.Gate;
        //            ret.description = online.Description;
        //            ret.authority = online.Authority;
        //            ret.refId = online.RefId;
        //            ret.success = online.Success;
        //        }

        //        var rets = "وزن سفارش: " + order.Weight + " ریال" + "جمع فاکتور: " + ret.orderTotal + " ریال" + " - هزینه ارسال: " + ret.shippingCost + " ریال" + " - جمع کل فاکتور: " + (ret.shippingCost + ret.orderTotal).ToString();
        //        rets += "<br/ >";
        //        if (ret.balanceWithdrawal != 0)
        //        {
        //            if (ret.balanceWithdrawal > 0)
        //            {
        //                rets += "بستانکاری: " + ret.balanceWithdrawal + " ریال";
        //            }
        //            else
        //            {
        //                rets += "بدهکاری: " + ret.balanceWithdrawal + " ریال";
        //            }
        //            rets += " - <b>جمع کل: " + ret.dargahP + " ریال</b>";

        //        }
        //        else
        //        {
        //            if (op.Type == 1)
        //            {
        //                rets += "<b>جمع کل: " + ret.dargahP + " ریال</b>";
        //            }
        //            else
        //            {
        //                rets += "<b>جمع کل: " + (ret.shippingCost + ret.orderTotal).ToString() + " ریال</b>";
        //            }
        //        }

        //        if (op.Type == 1)
        //        {
        //            rets += " (پرداخت شده - درگاه پرداخت اینترنتی)";
        //        }
        //        else
        //        {
        //            rets += " (پرداخت شده - واریز به حساب)";
        //        }

        //        return rets;
        //    }
        //    catch (Exception)
        //    {
        //        return "";
        //    }
        //}

        //[HttpGet]
        //public async Task<IActionResult> getCartdet(int Oid)
        //{
        //    try
        //    {
        //        var Items = await (from s in _context.Store_Cart
        //                           where s.Active == true && s.Order_Id == Oid
        //                           select s).ToListAsync();
        //        if (Items == null)
        //        {
        //            return Json(new { result = 0 });
        //        }

        //        var ret = new List<nes2>();
        //        foreach (var i in Items)
        //        {
        //            var n = await (from s in _context.Store_Item
        //                           where s.Id == i.Item_Id
        //                           select s.Title).FirstOrDefaultAsync();

        //            ret.Add(new nes2
        //            {
        //                count = i.Count,
        //                discount = i.Discount,
        //                name = n,
        //                price = i.Price,
        //                total = ((i.Price * i.Count) / 100) * (100 - i.Discount)
        //            });
        //        }

        //        return Json(new { result = ret });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = 0 });
        //    }
        //}

        public partial class nes3
        {
            public string name { get; set; }
            public string province { get; set; }
            public string city { get; set; }
            public string address { get; set; }
            public string zip { get; set; }
            public string mobile { get; set; }
            public string phone { get; set; }
        }

        //[HttpGet]
        //public async Task<IActionResult> getSchooldet(int Oid)
        //{
        //    try
        //    {
        //        var order = await (from s in _context.Store_Orders
        //                           where s.Id == Oid
        //                           select s).FirstOrDefaultAsync();
        //        if (order == null)
        //        {
        //            return Json(new { result = 0 });
        //        }

        //        var school = await (from s in _context.Schools
        //                            where s.Id == order.School_Id
        //                            select s).FirstOrDefaultAsync();
        //        var cp = new Service(_context).getcity(school.City_Id);

        //        var ret = new nes3
        //        {
        //            address = order.Address,
        //            mobile = order.Mobile,
        //            name = school.SchoolNameFa,
        //            phone = order.Phone,
        //            zip = order.ZipCode,
        //            city = cp.city,
        //            province = cp.province
        //        };

        //        return Json(new { result = ret });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = 0 });
        //    }
        //}

        public partial class nes4
        {
            public string name { get; set; }
            public string province { get; set; }
            public string city { get; set; }
            public string address { get; set; }
            public string zip { get; set; }
            public string mobile { get; set; }
            public string phone { get; set; }
            public List<nes2> buy { get; set; }

        }

        //[HttpGet]
        //public async Task<IActionResult> getPrint(int Oid)
        //{
        //    try
        //    {
        //        var order = await (from s in _context.Store_Orders
        //                           where s.Id == Oid
        //                           select s).FirstOrDefaultAsync();
        //        if (order == null)
        //        {
        //            return Json(new { result = 0 });
        //        }

        //        var school = await (from s in _context.Schools
        //                            where s.Id == order.School_Id
        //                            select s).FirstOrDefaultAsync();
        //        var cp = new Service(_context).getcity(school.City_Id);

        //        var ret = new nes4
        //        {
        //            address = order.Address,
        //            mobile = order.Mobile,
        //            name = school.SchoolNameFa + " -" + Shared.ReturnGender(school.Sex) + school.FnameFa + " " + school.LnameFa,
        //            phone = order.Phone,
        //            zip = await getPaymentinfo(order.Id),
        //            city = cp.city,
        //            province = cp.province
        //        };

        //        var Items = await (from s in _context.Store_Cart
        //                           where s.Active == true && s.Order_Id == Oid
        //                           select s).ToListAsync();
        //        if (Items == null)
        //        {
        //            return Json(new { result = 0 });
        //        }

        //        var ret2 = new List<nes2>();
        //        foreach (var i in Items)
        //        {
        //            var n = await (from s in _context.Store_Item
        //                           where s.Id == i.Item_Id
        //                           select s.Title).FirstOrDefaultAsync();

        //            ret2.Add(new nes2
        //            {
        //                count = i.Count,
        //                discount = i.Discount,
        //                name = n,
        //                price = i.Price,
        //                total = ((i.Price * i.Count) / 100) * (100 - i.Discount)
        //            });
        //        }
        //        ret.buy = ret2;

        //        return Json(new { result = ret });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = 0 });
        //    }
        //}

        //[HttpGet]
        //public async Task<IActionResult> getballog()
        //{
        //    try
        //    {
        //        var b = new List<Store_Balance2>();
        //        var a = await _context.Store_Balance.ToListAsync();
        //        foreach (var e in a)
        //        {
        //            var q = await (from s in _context.Schools
        //                           where s.Id == e.School_Id
        //                           orderby s.Id descending
        //                           select s).FirstOrDefaultAsync();

        //            b.Add(new Store_Balance2
        //            {
        //                Time = e.Time,
        //                Active = e.Active,
        //                Amount = e.Amount,
        //                Date = e.Date,
        //                Description = e.Description,
        //                Id = e.Id,
        //                Order_Id = e.Order_Id,
        //                School_Id = e.School_Id,
        //                SchoolCode = q.SchoolCode,
        //                SchoolNameFa = q.SchoolNameFa
        //            });
        //        }
        //        return Json(new { result = b });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = 0 });
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CABalLog(bool nowst, int who)
        //{
        //    try
        //    {
        //        bool impst = !nowst;

        //        var iwhochang = await (from s in _context.Store_Balance
        //                               where s.Id == who
        //                               select s).FirstOrDefaultAsync();
        //        iwhochang.Active = impst;

        //        await _context.SaveChangesAsync();
        //        if (impst)
        //        {
        //            return Json(new { result = "Log has been activated" });
        //        }
        //        else
        //        {
        //            return Json(new { result = "Log has been inactivated" });
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { result = "مشکلی رخ داده است" });
        //    }
        //}
    }
}