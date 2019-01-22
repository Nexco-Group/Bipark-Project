using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Ronisim.Models;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Ronisim.Controllers
{
    [Authorize]
    public class OrderEditController : Controller
    {
        private readonly ronisimContext _context;
        //static string rooturl = @"C:\\Website\\Contents";
        public OrderEditController(ronisimContext context, IHostingEnvironment environment)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            try
            {
                string ord = HttpContext.Session.GetString("OFSlOrder");            

                if (string.IsNullOrEmpty(ord))
                {
                    return Content("error1");
                }
                var schid = (from s in _context.Store_Orders
                                 where s.Id == int.Parse(ord)
                                 select s.School_Id).FirstOrDefault();

                Schools iUser = (from s in _context.Schools
                                 where s.Id == schid
                                 select s).FirstOrDefault();
                if (iUser == null)
                {
                    return Content("error2");
                }

                var PassToView = new PassToOrderEdit()
                {
                    iUser = iUser
                };

                PassToView.iStoreItems = (from s in _context.Store_Item
                                          where s.Active == true
                                          orderby s.VCV
                                          select s).ToList();

                PassToView.iNotification = Service.Getsystemnotification();

                var ss = new Service(_context).getcity(iUser.City_Id);
                PassToView.city = ss.city;
                PassToView.province = ss.province;


                var iorder = (from s in _context.Store_Orders
                                   where s.Id == int.Parse(ord)
                                   select s).LastOrDefault();
                if (iorder != null)
                {
                    ViewBag.orderExist = iorder.Id;
                    ViewBag.orderCodeExist = iorder.OrderCode;

                    ViewBag.iSht = iorder.ShippingType;
                    ViewBag.iShc = iorder.ShippingCost;

                    var buyitems = (from s in _context.Store_Cart
                                         where s.Active == true && s.Order_Id == iorder.Id
                                         select s).ToList();
                    string items = "";
                    for (var i = 0; i < buyitems.Count; i++)
                    {
                        if (i == 0)
                        {
                            items += buyitems[i].Item_Id + "-" + buyitems[i].Count;
                        }
                        else
                        {
                            items += "_" + buyitems[i].Item_Id + "-" + buyitems[i].Count;
                        }
                    }
                    ViewBag.orderCart = items;
                }
                else
                {
                    return Content("sry error occured :(");
                }

                ViewBag.balance = GetBalance(iUser.Id);

                return View(PassToView);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveInvoice(string json, int shippingType, bool infochange, EditInfoOrder info)
        {
            try
            {
                var items = json.Split('_');

                string mobile = HttpContext.Session.GetString("OFSlMobile");
                Schools iUser = (from s in _context.Schools
                                 where s.Mobile == mobile
                                 select s).FirstOrDefault();
                var order = new Store_Orders();

                if (infochange)
                {
                    if (ModelState.IsValid)
                    {
                        order = new Store_Orders
                        {
                            Active = true,
                            Address = info.address,
                            Mobile = info.mobile,
                            Phone = info.schphone,
                            SchoolCode = iUser.SchoolCode,
                            School_Id = iUser.Id,
                            ZipCode = info.zipcode,
                            Discount = 0,
                            OrderDate = Shared.GetDate(),
                            OrderTime = Shared.GetTime(),
                            OrderStep = 0
                        };
                        await _context.AddAsync(order);
                    }
                    else
                    {
                        string Message = string.Join(" - ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                        return Json(new { result = Message });
                    }
                }
                else
                {
                    order = new Store_Orders
                    {
                        Active = true,
                        Address = iUser.Address,
                        Mobile = iUser.Mobile,
                        Phone = iUser.Phone,
                        SchoolCode = iUser.SchoolCode,
                        School_Id = iUser.Id,
                        ZipCode = iUser.ZipCode,
                        Discount = 0,
                        OrderDate = Shared.GetDate(),
                        OrderTime = Shared.GetTime(),
                        OrderStep = 0,
                        ShippingType = shippingType,

                    };
                    await _context.AddAsync(order);
                }
                await _context.SaveChangesAsync();

                var stockerrors = "";
                var totalwithdiscount = 0;
                var weight = 0;

                foreach (var itm in items)
                {
                    var i = itm.Split('-');
                    var getitem = _context.Store_Item.Where(s => s.Id == int.Parse(i[0])).FirstOrDefault();

                    if (await GetStock(int.Parse(i[0])) < int.Parse(i[1]))
                    {
                        stockerrors += "</br>-" + getitem.Title;
                    }
                    var itmcart = new Store_Cart
                    {
                        Active = true,
                        Order_Id = order.Id,
                        Item_Id = int.Parse(i[0]),
                        Count = int.Parse(i[1]),
                        Price = getitem.Price,
                        Discount = getitem.Discount
                    };
                    //totalcost += itmcart.Count * itmcart.Price;
                    totalwithdiscount += ((itmcart.Count * itmcart.Price) / 100) * (100 - itmcart.Discount);
                    await _context.AddAsync(itmcart);

                    weight += (int)getitem.Weight * int.Parse(i[1]);
                }
                if (stockerrors != "")
                {
                    return Json(new { result = "موجودی کالا های زیر در انبار کافی نیست" + stockerrors + "<br/>لطفا سبد خرید را تغییر داده و یا برای افزایش موجودی با ما تماس بگیرید" });
                }
                order.TotalPrice = totalwithdiscount;
                order.OrderStep = 1;
                var OrderCode = (3000 + order.Id).ToString();

                int shc = 0;
                if (shippingType == 1)
                {
                    shc = GetShippingCost(weight, new Service(_context).getprovince(iUser.City_Id));
                    order.TotalPrice += shc;
                }

                order.Weight = weight;

                order.ShippingType = shippingType;
                order.ShippingCost = shc;
                order.OrderCode = OrderCode;

                await _context.SaveChangesAsync();

                return Json(new { result = 1, order = OrderCode, orderid = order.Id, shippingType, shippingCost = shc });

            }
            catch (Exception ex)
            {
                return Json(new { result = ex.ToString() });
            }
        }

        public int GetBalance(int? sch_id)
        {
            var blnc = (from s in _context.Store_Balance
                        where s.Active == true && s.School_Id == sch_id && s.Consider == true
                        select s).ToList();

            if (blnc == null)
            {
                return 0;
            }
            else
            {
                return blnc.Sum(c => c.Amount);
            }
        }

        public async Task<int> GetStock(int item_id)
        {
            var a = await (from s in _context.Store_Stock
                           where s.Active == true && s.Item_Id == item_id
                           select s).ToListAsync();

            return a.Sum(i => i.Number);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeInvoice(string json, int order_id, int shippingType, bool infochange, EditInfoOrder info)
        {
            try
            {
                var items = json.Split('_');

                string mobile = HttpContext.Session.GetString("OFSlMobile");
                Schools iUser = (from s in _context.Schools
                                 where s.Mobile == mobile
                                 select s).FirstOrDefault();

                var order = (from s in _context.Store_Orders
                             where s.Id == order_id
                             select s).FirstOrDefault();

                if (infochange)
                {
                    if (ModelState.IsValid)
                    {
                        order.Address = info.address;
                        order.Mobile = info.mobile;
                        order.Phone = info.schphone;
                        order.ZipCode = info.zipcode;
                    }
                    else
                    {
                        string Message = string.Join(" - ", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                        return Json(new { result = Message });
                    }
                }

                _context.Store_Cart.RemoveRange(_context.Store_Cart.Where(s => s.Order_Id == order.Id));

                //var totalcost = 0;
                var weight = 0;
                var totalwithdiscount = 0;
                var stockerrors = "";


                foreach (var itm in items)
                {
                    var i = itm.Split('-');
                    var getitem = _context.Store_Item.Where(s => s.Id == int.Parse(i[0])).FirstOrDefault();

                    if (await GetStock(int.Parse(i[0])) < int.Parse(i[1]))
                    {
                        stockerrors = "</br>-" + getitem.Title;
                    }

                    var itmcart = new Store_Cart
                    {
                        Active = true,
                        Order_Id = order.Id,
                        Item_Id = int.Parse(i[0]),
                        Count = int.Parse(i[1]),
                        Price = getitem.Price,
                        Discount = getitem.Discount
                    };
                    //totalcost += itmcart.Count * itmcart.Price;
                    totalwithdiscount += ((itmcart.Count * itmcart.Price) / 100) * (100 - itmcart.Discount);
                    await _context.AddAsync(itmcart);
                    weight += (int)getitem.Weight * int.Parse(i[1]);

                }
                if (stockerrors != "")
                {
                    return Json(new { result = "موجودی کالا های زیر در انبار کافی نیست" + stockerrors + "<br/>لطفا سبد خرید را تغییر داده و یا برای افزایش موجودی با ما تماس بگیرید" });
                }
                order.TotalPrice = totalwithdiscount;
                order.OrderStep = 1;

                int shc = 0;
                if (shippingType == 1)
                {
                    shc = GetShippingCost(weight, new Service(_context).getprovince(iUser.City_Id));
                    order.TotalPrice += shc;
                }

                order.ShippingType = shippingType;
                order.ShippingCost = shc;
                order.Weight = weight;

                await _context.SaveChangesAsync();

                return Json(new { result = 1, order = order.OrderCode, orderid = order.Id, shippingType, shippingCost = shc });
            }
            catch (Exception ex)
            {
                return Json(new { result = ex.ToString() });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LaghvInvoice()
        {
            try
            {
                string mobile = HttpContext.Session.GetString("OFSlMobile");
                Schools iUser = (from s in _context.Schools
                                 where s.Mobile == mobile
                                 select s).FirstOrDefault();

                var order = (from s in _context.Store_Orders
                             where s.Active == true && s.School_Id == iUser.Id && s.OrderStep < 2
                             select s).ToList();

                if (order != null)
                {
                    foreach (var or in order)
                    {
                        or.Active = false;
                    }
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return Json(new { result = "مشکلی پیش آمده است" });
                }
                return Json(new { result = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { result = ex.ToString() });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(int order_id, bool online)
        {
            try
            {
                string mobile = HttpContext.Session.GetString("OFSlMobile");
                Schools iUser = (from s in _context.Schools
                                 where s.Mobile == mobile
                                 select s).FirstOrDefault();


                var iOrder = (from s in _context.Store_Orders
                              where s.Id == order_id
                              select s).FirstOrDefault();
                var tp = (online == true) ? 1 : 0;
                var nowst = GetBalance(iUser.Id);
                var howmuch = 0;
                if (nowst != 0)
                {
                    var bal = new Store_Balance
                    {
                        Order_Id = iOrder.Id,
                        Active = false,
                        Amount = -nowst,
                        School_Id = iUser.Id,
                        Description = "Counted in order: " + iOrder.OrderCode,
                        Date = Shared.GetDate(),
                        Time = Shared.GetTime()
                    };
                    await _context.AddAsync(bal);
                    await _context.SaveChangesAsync();

                    var payment = new Store_OrderPayment
                    {
                        Order_Id = iOrder.Id,
                        Money = iOrder.TotalPrice - nowst,
                        Date = Shared.GetDate(),
                        Time = Shared.GetTime(),
                        Type = tp,
                        Active = true,
                        Balance_Log_Id = bal.Id
                    };
                    await _context.AddAsync(payment);
                    howmuch = iOrder.TotalPrice - nowst;
                }
                else
                {
                    var payment = new Store_OrderPayment
                    {
                        Order_Id = iOrder.Id,
                        Money = iOrder.TotalPrice,
                        Date = Shared.GetDate(),
                        Time = Shared.GetTime(),
                        Type = tp,
                        Active = true
                    };
                    await _context.AddAsync(payment);
                    howmuch = iOrder.TotalPrice;

                }

                if (online)
                {
                    await _context.SaveChangesAsync();

                    var paylink = Url.Action(nameof(ZarinPalPayment), "Orders", new { Order = Shared.Encrypt(iOrder.Id.ToString()) }, Request.Scheme);
                    return Json(new { result = 1, order = iOrder.OrderCode, paylink });
                }
                else
                {
                    iOrder.OrderStep = 2;
                    await _context.SaveChangesAsync();

                    string mes1 = @"وی سی وی" + "\n" + iUser.SchoolNameFa + " سفارش شما به شماره پیگیری " + iOrder.OrderCode + " ثبت شد و منتظر پردازش و تایید توسط بخش فروش می باشد" + "\n" + "لطفا مبلغ " + howmuch + " ریال را به کارت 6037997153996963 واریز کرده و شماره پیگیری و نام آموزشگاه را پیامک کنید ";
                    if (!string.IsNullOrEmpty(iUser.Email))
                    {
                        //Shared.SendEmail(iUser.FnameFa + " " + iUser.LnameFa, iUser.Email, mes1, "Your Order from VCV");
                    }
                    new Service(_context).SendSMS(iUser.Mobile, mes1);

                    string mes3 = @"وی سی وی" + "\n" + "آموزشگاه " + iUser.SchoolNameFa + "، " + Shared.ReturnGender(iUser.Sex) + iUser.FnameFa + " " + iUser.LnameFa + " سفارشی را به شماره پیگیری " + iOrder.OrderCode + " به ثبت رسانده است. پیگیری کنید ";

                    //Shared.SendEmail("VCV Gmail", "vcvenglish@gmail.com", mes3, "An order submitted");
                    new Service(_context).SendSMS("09204204700", mes3);
                    new Service(_context).SendSMS("09159714815", mes3);
                    return Json(new { result = 1, order = iOrder.OrderCode, pt = "واریز به حساب", sht = iOrder.ShippingType });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = ex.ToString() });
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RePayment(int order_id, bool online)
        {
            try
            {
                string mobile = HttpContext.Session.GetString("OFSlMobile");
                Schools iUser = (from s in _context.Schools
                                 where s.Mobile == mobile
                                 select s).FirstOrDefault();


                var iOrder = (from s in _context.Store_Orders
                              where s.Id == order_id
                              select s).FirstOrDefault();
                var tp = (online == true) ? 1 : 0;

                var nowst = await GetStock(iUser.Id);
                var howmuch = 0;
                if (nowst != 0)
                {
                    var bal = new Store_Balance
                    {
                        Order_Id = iOrder.Id,
                        Active = true,
                        Amount = -nowst,
                        School_Id = iUser.Id,
                        Description = "Counted in order: " + iOrder.OrderCode,
                        Date = Shared.GetDate(),
                        Time = Shared.GetTime()
                    };
                    await _context.AddAsync(bal);
                    await _context.SaveChangesAsync();

                    var payment = new Store_OrderPayment
                    {
                        Order_Id = iOrder.Id,
                        Money = iOrder.TotalPrice - nowst,
                        Date = Shared.GetDate(),
                        Time = Shared.GetTime(),
                        Type = tp,
                        Active = true,
                        Balance_Log_Id = bal.Id
                    };
                    await _context.AddAsync(payment);
                    howmuch = iOrder.TotalPrice - nowst;
                }
                else
                {
                    var payment = new Store_OrderPayment
                    {
                        Order_Id = iOrder.Id,
                        Money = iOrder.TotalPrice,
                        Date = Shared.GetDate(),
                        Time = Shared.GetTime(),
                        Type = tp,
                        Active = true
                    };
                    await _context.AddAsync(payment);
                    howmuch = iOrder.TotalPrice;
                }

                if (online)
                {
                    await _context.SaveChangesAsync();

                    var paylink = Url.Action(nameof(ZarinPalPayment), "Orders", new { Order = Shared.Encrypt(iOrder.Id.ToString()) }, Request.Scheme);
                    return Json(new { result = 1, order = iOrder.OrderCode, paylink });
                }
                else
                {
                    iOrder.OrderStep = 2;
                    await _context.SaveChangesAsync();

                    string mes1 = @"وی سی وی" + "\n" + iUser.SchoolNameFa + " سفارش شما به شماره پیگیری " + iOrder.OrderCode + "ثبت شد و منتظر پردازش و تایید توسط بخش فروش می باشد " + "/n" + "لطفا مبلغ " + howmuch + "را به کارت 6037997153996963 واریز کرده و شماره پیگیری و نام آموزشگاه را پیامک کنید ";
                    if (!string.IsNullOrEmpty(iUser.Email))
                    {
                        //Shared.SendEmail(iUser.FnameFa + " " + iUser.LnameFa, iUser.Email, mes1, "Your Order from VCV");
                    }
                    new Service(_context).SendSMS(iUser.Mobile, mes1);

                    string mes3 = @"وی سی وی" + "\n" + "آموزشگاه " + iUser.SchoolNameFa + "، " + Shared.ReturnGender(iUser.Sex) + iUser.FnameFa + " " + iUser.LnameFa + " سفارشی را به شماره پیگیری " + iOrder.OrderCode + " به ثبت رسانده است. پیگیری کنید ";

                    //Shared.SendEmail("VCV Gmail", "vcvenglish@gmail.com", mes3, "An order submitted");
                    new Service(_context).SendSMS("09204204700", mes3);
                    new Service(_context).SendSMS("09159714815", mes3);

                    return Json(new { result = 1, order = iOrder.OrderCode, pt = "واریز به حساب", sht = iOrder.ShippingType });
                }
            }
            catch (Exception ex)
            {
                return Json(new { result = ex.ToString() });
            }
        }


        public class iPayment
        {
            public int Amount { get; set; }
            public string Description { get; set; }
            public string Email { get; set; }
            public string Mobile { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> ZarinPalPayment(string Order, string dargah)
        {
            var iOrder = (from s in _context.Store_Orders
                          where s.Id == int.Parse(Shared.Decrypt(Order))
                          select s).FirstOrDefault();
            if (iOrder == null)
            {
                return Content("مشکلی رخ داده");
            }

            var model = new iPayment
            {
                Amount = (iOrder.TotalPrice - GetBalance(iOrder.School_Id)) / 10,
                Description = "سفارش " + iOrder.OrderCode + " وی سی وی",
                Email = "",
                Mobile = iOrder.Mobile
            };

            //مرچنت کد خود را وارد کنید
            var payment = await new Zarinpal.Payment("b689de60-6707-11e8-a4b2-005056a205be", model.Amount)
                .PaymentRequest(model.Description,
                    Url.Action(nameof(ZarinPalPaymentVerify), "Orders", new { Order }, Request.Scheme),
                    model.Email,
                    model.Mobile);
            //در صورت موفق آمیز بودن درخواست، کاربر به صفحه پرداخت هدایت می شود
            //در غیر این صورت خطا نمایش داده شود
            return payment.Status == 100 ? (IActionResult)Redirect(payment.Link + "/Sad") : BadRequest($"خطا در پرداخت. کد خطا:{payment.Status}");
        }

        public int GetShippingCost(int Weight, int Province)
        {
            try
            {
                if (Province == 11)
                {
                    var p = 58000;
                    if (Weight <= 5000)
                    {
                        return (int)Math.Ceiling(p * 1.09);
                    }
                    else
                    {
                        var w = Weight - 5000;
                        double how = (w / 1000);
                        return (int)Math.Ceiling(((((w / 1000) + 1) * 8000) + p) * 1.09);
                    }
                }
                else
                {
                    var p = 83000;
                    if (Weight <= 5000)
                    {
                        return (int)Math.Ceiling(p * 1.09);
                    }
                    else
                    {
                        var w = Weight - 5000;
                        return (int)Math.Ceiling(((((w / 1000) + 1) * 8000) + p) * 1.09);
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public async Task<IActionResult> ZarinPalPaymentVerify(string Order, string Authority, string Status)
        {
            try
            {
                if (string.IsNullOrEmpty(Order))
                {
                    RedirectToAction("Orders");
                }
                var iOrder = (from s in _context.Store_Orders
                              where s.Id == int.Parse(Shared.Decrypt(Order))
                              select s).FirstOrDefault();

                if (iOrder.Active != true)
                {
                    RedirectToAction("Orders");
                }


                Schools iUser = (from s in _context.Schools
                                 where s.Id == iOrder.School_Id
                                 select s).FirstOrDefault();

                if (iOrder == null)
                {
                    return Content("مشکلی رخ داده");
                }
                ViewBag.orderFinish = iOrder.Id;
                ViewBag.orderCodeFinish = iOrder.OrderCode;

                ViewBag.iSht = iOrder.ShippingType;
                ViewBag.iShc = iOrder.ShippingCost;

                var buyitems = (from s in _context.Store_Cart
                                where s.Active == true && s.Order_Id == iOrder.Id
                                select s).ToList();
                string items = "";
                for (var i = 0; i < buyitems.Count; i++)
                {
                    if (i == 0)
                    {
                        items += buyitems[i].Item_Id + "-" + buyitems[i].Count;
                    }
                    else
                    {
                        items += "_" + buyitems[i].Item_Id + "-" + buyitems[i].Count;
                    }
                }
                ViewBag.orderFinishCart = items;

                var pym = (from s in _context.Store_OrderPayment
                           where s.Active == true && s.Order_Id == iOrder.Id
                           select s).LastOrDefault();

                if (Status == "NOK")
                {
                    ViewBag.orderMes = "فرایند پرداخت دراگاه بانکی موفقیت آمیز نبود";
                    ViewBag.orderSuccess = "false";
                    ViewBag.pym = Shared.Encrypt(pym.Id.ToString());
                    ViewBag.balance = GetBalance(iUser.Id);

                    var pymexist = (from s in _context.Store_Payment_Online
                                    where s.Authority == Authority.TrimStart('0')
                                    select s).LastOrDefault();

                    if (pymexist == null)
                    {
                        var payo = new Store_Payment_Online
                        {
                            Amount = pym.Money,
                            Authority = Authority.TrimStart('0'),
                            Gate = "ZarinGate",
                            Payment_Id = pym.Id,
                            Success = false,
                            Description = "Status:NOK"
                        };
                        await _context.AddAsync(payo);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    var zi = new Zarinpal.Payment("b689de60-6707-11e8-a4b2-005056a205be", (pym.Money / 10));
                    var verification = await zi.Verification(Authority);
                    //ارسال به صفحه خطا
                    if (verification.Status == 100)
                    {
                        ViewBag.balance = GetBalance(iUser.Id);

                        string mes1 = @"وی سی وی" + "\n" + iUser.SchoolNameFa + " سفارش شما به شماره پیگیری " + iOrder.OrderCode + " ثبت/پرداخت شد و منتظر پردازش و تایید توسط بخش فروش می باشد";
                        if (!string.IsNullOrEmpty(iUser.Email))
                        {
                            //Shared.SendEmail(iUser.FnameFa + " " + iUser.LnameFa, iUser.Email, mes1, "Your Order from VCV");
                        }
                        new Service(_context).SendSMS(iUser.Mobile, mes1);

                        string mes3 = @"وی سی وی" + "\n" + "آموزشگاه " + iUser.SchoolNameFa + "، " + Shared.ReturnGender(iUser.Sex) + iUser.FnameFa + " " + iUser.LnameFa + " سفارشی را به شماره پیگیری " + iOrder.OrderCode + " به ثبت رسانده است. پیگیری کنید ";

                        //Shared.SendEmail("VCV Gmail", "vcvenglish@gmail.com", mes3, "An order submitted");
                        new Service(_context).SendSMS("09204204700", mes3);
                        new Service(_context).SendSMS("09159714815", mes3);

                        //ارسال کد تراکنش به جهت نمایش به کاربر
                        var refId = verification.RefId;
                        ViewBag.orderMes = "پرداخت با موفقیت انجام شد. کد پرداخت: " + refId.ToString();
                        ViewBag.orderSuccess = "true";

                        var pymexist = (from s in _context.Store_Payment_Online
                                        where s.Authority == Authority.TrimStart('0')
                                        select s).LastOrDefault();

                        if (pymexist == null)
                        {
                            var payo = new Store_Payment_Online
                            {
                                Amount = pym.Money,
                                Authority = Authority.TrimStart('0'),
                                Gate = "ZarinGate",
                                Payment_Id = pym.Id,
                                Success = true,
                                Description = "Status:100",
                                RefId = refId.ToString()
                            };
                            await _context.AddAsync(payo);
                        }

                        // cost bal
                        if (pym.Balance_Log_Id != 0)
                        {
                            var bal = (from s in _context.Store_Balance
                                       where s.Id == pym.Balance_Log_Id && s.Active != true
                                       select s).LastOrDefault();

                            if (bal != null)
                                bal.Active = true;
                        }

                        iOrder.OrderStep = 2;
                        await _context.SaveChangesAsync();
                    }
                    else if (verification.Status == 101)
                    {
                        ViewBag.balance = GetBalance(iUser.Id);

                        ViewBag.orderMes = "پرداخت با موفقیت انجام شد ";
                        ViewBag.orderSuccess = "true";
                    }
                    else
                    {
                        ViewBag.balance = GetBalance(iUser.Id);

                        ViewBag.orderMes = "فرایند پرداخت دراگاه بانکی موفقیت آمیز نبود. کد خطا: " + verification.RefId + "--" + verification.Status;
                        ViewBag.orderSuccess = "false";

                        var pymexist = (from s in _context.Store_Payment_Online
                                        where s.Authority == Authority.TrimStart('0')
                                        select s).LastOrDefault();

                        if (pymexist == null)
                        {
                            var payo = new Store_Payment_Online
                            {
                                Amount = pym.Money,
                                Authority = Authority.TrimStart('0'),
                                Gate = "ZarinGate",
                                Payment_Id = pym.Id,
                                Success = false,
                                Description = "Status:" + verification.Status
                            };
                            await _context.AddAsync(payo);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            return RedirectToAction("Index", "Orders");
                        }
                    }

                }

                var PassToView = new PassToOrders()
                {
                    iUser = iUser
                };

                PassToView.iStoreItems = (from s in _context.Store_Item
                                          where s.Active == true
                                          orderby s.VCV
                                          select s).ToList();

                PassToView.iMessage = new Service(_context).getNewMessage(iUser.Id, "20");
                PassToView.iNotification = Service.Getsystemnotification();

                var ss = new Service(_context).getcity(iUser.City_Id);
                PassToView.city = ss.city;
                PassToView.province = ss.province;
                return View("Index", PassToView);
            }
            catch (Exception ex)
            {
                return Content("<h1 style='text-align: center'>مشکلی پیش آمده است. لطفا با پشتیبانی در تماس باشید</h1>" + ex.ToString());
            }
        }

    }
}