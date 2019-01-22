using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ronisim.Models;
using System.Globalization;
using static Ronisim.Controllers.ManagementController;
using System.Text;

public class Service
{
    static string hosturlcontents = "https://contents.vcv.ir";
    private readonly ronisimContext _context;
    public Service(ronisimContext context)
    {
        _context = context;

    }
    public static string GetHTMLString(List<Request_plus> s,int m1, int m2, float gig)
    {
        var sb = new StringBuilder();
        sb.Append(@"
                        <html>
                            <head>
                                    <style>
body {
    font-size: 14px;
    font-family: Tahoma, Helvetica, sans-serif;
}

                                     </style>
                            </head>
                            <body>                                
                                <table dir='rtl' cellspacing='50'>
                                    <tr>
                                        <th>شماره پیگیری رزرو</th>
                                        <th>نام کاربر</th>
                                        <th>کد کاربر</th>
                                        <th>جای پارک رزروی</th>
                                        <th>موبایل</th>
                                        <th>قیمت</th>
                                        <th>قیمت مصرف کننده</th>
                                        <th>سود</th>
                                        <th>حجم</th>
                                        <th>توضیحات</th>
                                        <th>اپراتور</th>
                                        <th>تاریخ رزرو</th>
                                        <th>وضعیت</th>
                                    </tr>");

        foreach (var emp in s)
        {
            sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                    <td>{6}</td>
                                    <td>{7}</td>
                                    <td>{8}</td>
                                    <td>{9}</td>
                                    <td>{10}</td>
                                    <td>{11}</td>
                                    <td>{12}</td>

                                  </tr>", emp.RequestCode, emp.Who, emp.whoCode, emp.Pack, emp.ForMobile, Shared.joda(emp.money), Shared.joda(emp.money2), Shared.joda(emp.money2 - emp.money), emp.gig, emp.note, emp.whoVerify, emp.OrderDate, emp.Status);
        }
        sb.Append(@"
                                </table>
                            </body>
                        </html><div style='text-align:center'><b>کل فروش: " + Shared.joda(m1) + " ریال</b> <b>کل فروش - قیمت مصرف کننده: " + Shared.joda(m2) + " ریال</b> <b>کل سود: " + Shared.joda(m2-m1) + " ریال</b> <b>کل حجم فروحته شده: " + gig + " گیگابایت</b></div>");

        return sb.ToString();
    }


    public static string GetHTMLString2(List<Balance> s)
    {
        var sb = new StringBuilder();
        sb.Append(@"
                        <html>
                            <head>
                                    <style>
body {
    font-size: 14px;
    font-family: Tahoma, Helvetica, sans-serif;
}
                                     </style>
                            </head>
                            <body>                                
                                <table dir='rtl' cellspacing='50'>
                                    <tr>
                                        <th>شماره پیگیری</th>
                                        <th>نام فرد</th>
                                        <th>کد فرد</th>
                                        <th>مبلغ</th>
                                        <th>نوع تراکنش</th>
                                        <th>توضیح تراکنش</th>
                                        <th>تاریخ</th>
                                        <th>زمان</th>
                                    </tr>");

        foreach (var emp in s)
        {
            sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                    <td>{6}</td>
                                    <td>{7}</td>
                                  </tr>", emp.Id, emp.Who, emp.WhoId, Shared.joda(emp.Amount), (emp.Amount > 0) ? "واریز مبلغ - افزایش موجودی" : "کسر مبلغ - بابت رزرو", emp.Description, emp.Date, emp.Time);
        }
        sb.Append(@"
                                </table>
                            </body>
                        </html>");

        return sb.ToString();
    }


    public static string GetHTMLString3(List<Balance> s, int total, int inc, int outc)
    {
        var sb = new StringBuilder();
        sb.Append(@"
                        <html>
                            <head>
                                    <style>
body {
    font-size: 14px;
    font-family: Tahoma, Helvetica, sans-serif;
}

                                     </style>
                            </head>
                            <body>                                
                                <table dir='rtl' cellspacing='50'>
                                    <tr>
                                        <th>شماره پیگیری</th>
                                        <th>مبلغ</th>
                                        <th>نوع تراکنش</th>
                                        <th>توضیح تراکنش</th>
                                        <th>وضعیت</th>
                                        <th>تاریخ</th>
                                        <th>زمان</th>
                                    </tr>");


        foreach (var emp in s)
        {
            sb.AppendFormat(@"<tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2}</td>
                                    <td>{3}</td>
                                    <td>{4}</td>
                                    <td>{5}</td>
                                    <td>{6}</td>
                                  </tr>", emp.Id, Shared.joda(emp.Amount), (emp.Amount > 0) ? "واریز مبلغ - افزایش موجودی" : "کسر مبلغ - بابت رزرو", (emp.Consider == false || emp.Active == false) ? "ناموفق - لغو شده - بررسی نشده" : "موفق - حساب شده در موجودی", emp.Description, emp.Date, emp.Time);
        }
        sb.Append(@"
                                </table>
                            </body>
                        </html><div style='text-align:center'><b>ورودی: " + Shared.joda(inc) + " ریال</b> <b>خروجی: "+ Shared.joda(outc) + " ریال</b> <b>کل مبلغ: "+ Shared.joda(total) + " ریال</b></div>");

        return sb.ToString();
    }

    public async Task<bool> SSHelpAsync(HttpWebRequest request)
    {
        WebResponse response = await request.GetResponseAsync();
        Stream stream = response.GetResponseStream();
        StreamReader reader = new StreamReader(stream);
        string htmlText = await reader.ReadToEndAsync();
        return htmlText == "1-0";
    }

    //public class dcp
    //{
    //    public string city { get; set; }
    //    public string province { get; set; }
    //}

    //public dcp getcity(int id)
    //{
    //    var b = (from s in _context.City
    //             where s.Id == id
    //             select s).FirstOrDefault();
    //    if(b == null)
    //    {
    //        return null;
    //    }
    //    return new dcp
    //    {
    //        city = b.Name
    //                ,
    //        province = (from s in _context.Province
    //                    where s.Id == b.Province_Id
    //                    select s.Name).FirstOrDefault()
    //    };
    //}

    //public string getcity2(int id)
    //{
    //    var b = (from s in _context.City
    //             where s.Id == id
    //             select s).FirstOrDefault();
    //    return (b == null || string.IsNullOrEmpty(b.Name)) ? "NaN" : b.Name;
    //}
    //public async Task<List<int>> getmyteachersid(int schid)
    //{
    //    return await (from s in _context.Teachers
    //                  where s.SchoolId == schid && s.Active == true
    //                  select s.Id).ToListAsync();

    //}
    //public async Task<List<Classes2>> getmyactiveclasses(int schid)
    //{
    //    var a = (await (from s in _context.Classes
    //                    where s.SchoolId == schid && (s.Active == true)
    //                    orderby s.Id descending
    //                    select s).ToListAsync());

    //    var ret = new List<Classes2>();
    //    foreach (var et in a)
    //    {
    //        var tch = await (from s in _context.Teachers
    //                         where s.Id == et.TeacherId
    //                         select s).FirstOrDefaultAsync();
    //        var level = et.Level + "[" + et.LevelExtoEx + "]";
    //        if (!string.IsNullOrEmpty(et.Level2)) { level += " " + et.Level2 + "[" + et.Level2ExtoEx + "]"; }

    //        ret.Add(new Classes2
    //        {
    //            Id = et.Id,
    //            ClassCode = et.ClassCode,
    //            TeacherName = tch.FnameFa + " " + tch.LnameFa,
    //            TeacherId = tch.Id,
    //            Level = level,
    //            Level1 = et.Level,
    //            Level1ExtoEx = et.LevelExtoEx,
    //            Level2 = et.Level2,
    //            Level2ExtoEx = et.Level2ExtoEx,
    //            Capacity = et.Capacity,
    //            StartDate = et.StartDate,
    //            FinalDate = et.FinalDate,
    //            Active = et.Active
    //        });
    //    }
    //    return ret;
    //}
    //public async Task<List<Classes2>> getmydisactiveclasses(int schid)
    //{
    //    var a = (await (from s in _context.Classes
    //                    where s.SchoolId == schid && (s.Active != true)
    //                    orderby s.Id descending
    //                    select s).ToListAsync());

    //    var ret = new List<Classes2>();
    //    foreach (var et in a)
    //    {
    //        var tch = await (from s in _context.Teachers
    //                         where s.Id == et.TeacherId
    //                         select s).FirstOrDefaultAsync();
    //        var level = et.Level + "[" + et.LevelExtoEx + "]";
    //        if (!string.IsNullOrEmpty(et.Level2)) { level += " " + et.Level2 + "[" + et.Level2ExtoEx + "]"; }

    //        ret.Add(new Classes2
    //        {
    //            Id = et.Id,
    //            ClassCode = et.ClassCode,
    //            TeacherName = tch.FnameFa + " " + tch.LnameFa,
    //            TeacherId = tch.Id,
    //            Level = level,
    //            Level1 = et.Level,
    //            Level1ExtoEx = et.LevelExtoEx,
    //            Level2 = et.Level2,
    //            Level2ExtoEx = et.Level2ExtoEx,
    //            Capacity = et.Capacity,
    //            StartDate = et.StartDate,
    //            FinalDate = et.FinalDate,
    //            Active = et.Active
    //        });
    //    }
    //    return ret;
    //}

    //public async Task<List<int>> getmyclassesid(int schid)
    //{
    //    return await (from s in _context.Classes
    //                  where s.SchoolId == schid && s.Active == true
    //                  select s.Id).ToListAsync();
    //}
    //public async Task<List<Students>> getmystudents(int schid)
    //{
    //    var cl = await getmyclassesid(schid);

    //    var std = await (from s in _context.StudentRegs
    //                     where cl.Contains(s.ClassId) && s.Active == true
    //                     select s.StudentId).ToListAsync();

    //    return await (from s in _context.Students
    //                  where std.Any(sss => sss == s.Id)
    //                  orderby s.Id descending
    //                  select s).ToListAsync();//StudentToView
    //}

    //public async Task<List<Classes>> GetClassOfSchool(int schid)
    //{
    //    return await (from s in _context.Classes
    //                  where s.SchoolId == schid && s.Active == true
    //                  select s).ToListAsync();
    //}
    //public Excercise GetExcersice(string Level, string Lesson, string Exercise)
    //{
    //    return (from s in _context.Excercise
    //            where s.Level == Level && s.Lesson == Lesson && s.Exercise == Exercise
    //            select s)
    //            .FirstOrDefault();
    //}

    //public int GetLessonExNo(string Level, string Lesson)
    //{
    //    return (from s in _context.Excercise
    //            where s.Level == Level && s.Lesson == Lesson
    //            select s.Id)
    //            .ToList().Count;
    //}

    //public Classes LastTerm(int stdid)
    //{
    //    var clas = (from s in _context.StudentRegs
    //                where s.StudentId == stdid
    //                orderby s.Id descending
    //                select s).ToList();

    //    if (clas.Count > 1)
    //    {
    //        return (from s in _context.Classes
    //                where s.Id == clas[1].ClassId
    //                select s).FirstOrDefault();
    //    }
    //    else { return null; }
    //}


    //public string GetNameOfRole(int id, int code)
    //{
    //    try
    //    {
    //        if (code == 0)
    //        {
    //            return "";
    //        }
    //        else if (code == 10)
    //        {
    //            var i = (from s in _context.Admins
    //                     where s.Id == id
    //                     select s).FirstOrDefault();
    //            return i.FnameFa + " " + i.LnameFa;
    //        }
    //        else if (code == 20)
    //        {
    //            var i = (from s in _context.Admins
    //                     where s.Id == id
    //                     select s).FirstOrDefault();
    //            return i.FnameFa + " " + i.LnameFa;
    //        }
    //        else if (code == 30)
    //        {
    //            var i = (from s in _context.Teachers
    //                     where s.Id == id
    //                     select s).FirstOrDefault();
    //            return i.FnameFa + " " + i.LnameFa;
    //        }
    //        else
    //        {
    //            var i = (from s in _context.Students
    //                     where s.Id == id
    //                     select s).FirstOrDefault();

    //            var c = (from s in _context.StudentRegs
    //                     where s.StudentId == i.Id
    //                     select s).LastOrDefault();

    //            var cls = (from s in _context.Classes
    //                       where s.Id == c.ClassId
    //                       select s).LastOrDefault();

    //            return i.FnameFa + " " + i.LnameFa + " Class Code: " + cls.ClassCode;
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        return "unknown";
    //    }
    //}


    //public static List<string> Getsystemnotification()
    //{
    //    return new List<string>() { };
    //}

    //public List<Message> getNewMessage(int stdid, string who)
    //{
    //    var doex = (from s in _context.Chats
    //                where s.ToCode == who && s.Seen == false
    //                select s)
    //              .ToList();

    //    var ss = new List<Message>();
    //    foreach (var s in doex)
    //    {
    //        ss.Add(new Message
    //        {
    //            mesName = GetNameOfRole(s.FromId, int.Parse(s.FromCode)) + "(" + Shared.GetRoleOfCode(int.Parse(s.FromCode)) + ")",
    //            imgUrl = GetImageOfRole(s.FromId, int.Parse(s.FromCode)),
    //            mesText = (s.MessageType != "text") ? s.MessageType + " file" : ((s.MessageContent.Length < 45) ? s.MessageContent : s.MessageContent.Substring(0, 45) + " ..."),
    //            mesTime = s.SendDate + " | " + s.SendTime
    //        });
    //    }

    //    return ss;
    //}

    //public string GetImageOfRole(int id, int code)
    //{
    //    try
    //    {
    //        if (code == 0)
    //        {
    //            return hosturlcontents + "/ProfileImage/VCV-Support.png";

    //        }
    //        else if (code == 10)
    //        {
    //            var i = (from s in _context.Admins
    //                     where s.Id == id
    //                     select s).FirstOrDefault();
    //            return (string.IsNullOrEmpty(i.ImagePath)) ? (hosturlcontents + "/ProfileImage/" + ((i.Sex) ? "Man" : "Woman") + ".png") : hosturlcontents + "/ProfileImage/" + i.ImagePath;
    //        }
    //        else if (code == 20)
    //        {
    //            var i = (from s in _context.Admins
    //                     where s.Id == id
    //                     select s).FirstOrDefault();
    //            return (string.IsNullOrEmpty(i.ImagePath)) ? (hosturlcontents + "/ProfileImage/" + ((i.Sex) ? "Man" : "Woman") + ".png") : hosturlcontents + "/ProfileImage/" + i.ImagePath;
    //        }
    //        else if (code == 30)
    //        {
    //            var i = (from s in _context.Teachers
    //                     where s.Id == id
    //                     select s).FirstOrDefault();
    //            return (string.IsNullOrEmpty(i.ImagePath)) ? (hosturlcontents + "/ProfileImage/" + ((i.Sex) ? "Man" : "Woman") + ".png") : hosturlcontents + "/ProfileImage/" + i.ImagePath;
    //        }
    //        else
    //        {
    //            var i = (from s in _context.Students
    //                     where s.Id == id
    //                     select s).FirstOrDefault();
    //            return (string.IsNullOrEmpty(i.ImagePath)) ? (hosturlcontents + "/ProfileImage/" + ((i.Sex) ? "Boy" : "Girl") + ".png") : hosturlcontents + "/ProfileImage/" + i.ImagePath;
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        return hosturlcontents + "/ProfileImage/VCV-Support.png";
    //    }
    //}

    //public string GetImageOfRole2(int id, int code)
    //{
    //    try
    //    {
    //        if (code == 0)
    //        {
    //            return hosturlcontents2 + "/ProfileImage/VCV-Support.png";

    //        }
    //        else if (code == 10)
    //        {
    //            var i = (from s in _context.Admins
    //                     where s.Id == id
    //                     select s).FirstOrDefault();
    //            return (string.IsNullOrEmpty(i.ImagePath)) ? (hosturlcontents2 + "/ProfileImage/" + ((i.Sex) ? "Man" : "Woman") + ".png") : hosturlcontents2 + "/ProfileImage/" + i.ImagePath;
    //        }
    //        else if (code == 20)
    //        {
    //            var i = (from s in _context.Admins
    //                     where s.Id == id
    //                     select s).FirstOrDefault();
    //            return (string.IsNullOrEmpty(i.ImagePath)) ? (hosturlcontents2 + "/ProfileImage/" + ((i.Sex) ? "Man" : "Woman") + ".png") : hosturlcontents2 + "/ProfileImage/" + i.ImagePath;
    //        }
    //        else if (code == 30)
    //        {
    //            var i = (from s in _context.Teachers
    //                     where s.Id == id
    //                     select s).FirstOrDefault();
    //            return (string.IsNullOrEmpty(i.ImagePath)) ? (hosturlcontents2 + "/ProfileImage/" + ((i.Sex) ? "Man" : "Woman") + ".png") : hosturlcontents2 + "/ProfileImage/" + i.ImagePath;
    //        }
    //        else
    //        {
    //            var i = (from s in _context.Students
    //                     where s.Id == id
    //                     select s).FirstOrDefault();
    //            return (string.IsNullOrEmpty(i.ImagePath)) ? (hosturlcontents2 + "/ProfileImage/" + ((i.Sex) ? "Boy" : "Girl") + ".png") : hosturlcontents2 + "/ProfileImage/" + i.ImagePath;
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        return hosturlcontents2 + "/ProfileImage/VCV-Support.png";
    //    }
    //}
}

