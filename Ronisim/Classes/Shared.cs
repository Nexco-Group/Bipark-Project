using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class Shared
{
    static string hosturl = "https://contents.vcv.ir";
    static string rooturl = @"C:\\Website\\Contents";
    private static Random random = new Random();
    private static Random random1 = new Random();
    public static string GetSalt()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 20)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string GetWhoSend()
    {
        return "09355725000";

        //var now = DateTime.Now.Hour;
        //if (7 <= now && now <= 15)
        //{
        //    return "09352515757";
        //}
        //else if (15 <= now && now <= 23)
        //{
        //    return "09354978000";
        //}
        //else
        //{
        //    return "09355725000";
        //}
    }

    public static string joda(int? inc)
    {
        return string.Format("{0:n0}", inc.Value);
    }
    public static bool SendSMS(string SMS_Mobile, string SMS_Text)
    {
        return false;
        //try
        //{
        //    SmsIrRestfulNetCore.Token tk = new SmsIrRestfulNetCore.Token();
        //    var token = tk.GetToken("f6c863973c000010dc5a6397", "ronisim615243");

        //    SmsIrRestfulNetCore.MessageSend ms = new SmsIrRestfulNetCore.MessageSend();

        //    var res = ms.Send(token, new SmsIrRestfulNetCore.MessageSendObject()
        //    {
        //        MobileNumbers = new List<string>() { SMS_Mobile }.ToArray(),
        //        Messages = new List<string>() { SMS_Text }.ToArray(),
        //        LineNumber = "50002015444468",
        //        SendDateTime = null,
        //        CanContinueInCaseOfError = true
        //    });
        //    return true;
        //}
        //catch (Exception)
        //{
        //    return false;
        //}
    }

    public static string getb(int? a)
    {
        if (a == 1)
        {
            return "مگابایت";
        }
        else
        {
            return "گیگابایت";
        }
    }

    public static bool GetDDif(string date, int howmsub)
    {
        TimeSpan diff = new TimeSpan((okdatetime(GetDate(), GetTime()) - okdatetime(date, "00:00")).Days, 0, 0, 0);

        return (diff.Days <= howmsub);
    }

    public static string SafeFarsiStr(string input)
    {
        return input.Replace("ي", "ی").Replace("ئ", "ی").Replace("ك", "ک");
    }
    public static String GetHash(String value)
    {
        using (SHA256 hash = SHA256.Create())
        {
            return String.Concat(hash
              .ComputeHash(Encoding.UTF8.GetBytes(value))
              .Select(item => item.ToString("x2")));
        }
    }

    public static string Ok_String(string sInput)
    {
        if (string.IsNullOrEmpty(sInput))
        {
            return "";
        }
        else
        {
            return sInput.Replace("ك", "ک").Replace("ي", "ی").Replace("ي", "ی").Replace("ء", "").Trim();
        }
    }

    public static string Encrypt(string encryptString)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                }
                encryptString = Convert.ToBase64String(ms.ToArray());
            }
        }
        return encryptString;
    }

    public static string Decrypt(string cipherText)
    {
        string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
            });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    public static string GetAvatarGeneral(string image, bool sex)
    {
        if (!string.IsNullOrEmpty(image))
        {
            if (File.Exists(Path.Combine(rooturl, "ProfileImage", image)))
            {
                return hosturl + "/ProfileImage/" + image;
            }
        }

        if (sex)
        {
            return hosturl + "/ProfileImage/Man.png";
        }
        else
        {
            return hosturl + "/ProfileImage/Woman.png";
        }
    }

    public static int GetNowYear()
    {
        DateTime miladi = DateTime.Now;
        PersianCalendar shamsi = new PersianCalendar();
        return shamsi.GetYear(miladi);
    }


    //public static bool SendEmail(string toname, string toemail, string messagetext, string messagetitle, string link, string linktext, string messagesubject)
    //{
    //    try
    //    {
    //        var message = new MimeMessage();
    //        message.From.Add(new MailboxAddress("Ronisim Notification", "info@vcv.ir"));
    //        message.To.Add(new MailboxAddress(toname, toemail));
    //        message.Subject = messagesubject;

    //        var body = "";
    //        using (StreamReader reader = File.OpenText(@"C:\\Website\\Contents\\email.txt"))
    //        {
    //            body = reader.ReadToEnd();
    //            //Replace UserName and Other variables available in body Stream
    //            body = body.Replace("{body}", messagetext);
    //            body = body.Replace("{title}", messagetitle);
    //            body = body.Replace("{link}", link);
    //            body = body.Replace("{linktext}", linktext);
    //        }

    //        message.Body = new TextPart("html") { Text = body };

    //        using (var client = new SmtpClient())
    //        {
    //            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
    //            client.Connect("vcv.ir", 465, true);
    //            client.Authenticate("info@vcv.ir", "dViFWUsa@mWz");
    //            client.Send(message);
    //            client.Disconnect(true);
    //        }
    //        return true;
    //    }
    //    catch (Exception)
    //    {
    //        return false;
    //    }
    //}

    public static string IsValidNationalCode(string nationalCode)
    {
        //در صورتی که کد ملی وارد شده تهی باشد

        if (String.IsNullOrEmpty(nationalCode))
            return "کد ملی وارد شده اشتباه است";


        //در صورتی که کد ملی وارد شده طولش کمتر از 10 رقم باشد
        if (nationalCode.Length != 10)
            return "کد ملی وارد شده اشتباه است";

        //در صورتی که کد ملی ده رقم عددی نباشد
        var regex = new Regex(@"\d{10}");
        if (!regex.IsMatch(nationalCode))
            return "کد ملی وارد شده اشتباه است";

        //در صورتی که رقم‌های کد ملی وارد شده یکسان باشد
        var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
        if (allDigitEqual.Contains(nationalCode)) return "کد ملی وارد شده اشتباه است";


        //عملیات شرح داده شده در بالا
        var chArray = nationalCode.ToCharArray();
        var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
        var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
        var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
        var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
        var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
        var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
        var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
        var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
        var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
        var a = Convert.ToInt32(chArray[9].ToString());

        var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
        var c = b % 11;

        if ((((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a))))
        {
            return "1";
        }
        else
        {
            return "کد ملی وارد شده اشتباه است";
        }
    }
    public static string GetDate()
    {
        DateTime miladi = DateTime.Now;
        PersianCalendar shamsi = new PersianCalendar();
        return string.Format("{0}/{1}/{2}", shamsi.GetYear(miladi), shamsi.GetMonth(miladi), shamsi.GetDayOfMonth(miladi));
        //var dtd = DateTime.Now;
        //DateTime dt = new DateTime(dtd.Year, dtd.Month, dtd.Day, new PersianCalendar());
        //return DateTime.Now.ToString("yyyy/MM/dd", new CultureInfo("fa-IR"));
    }
    public static string GetTime()
    {
        return DateTime.Now.ToString("HH:mm");
    }
    public static string GetMobilefix(string mobile)
    {
        return mobile.Replace(" ", "").Replace("-", "");
    }
    public static bool LevelIsCorrect(string level)
    {
        return new Regex(@"VCV\d{1}").IsMatch(level);
    }
    public static bool MobileIsCorrect(string mobile)
    {
        return new Regex(@"09\d{9}").IsMatch(mobile);
    }
    public static bool extoexIsCorrect(string extoex)
    {
        try
        {
            var a = extoex.Split('-');
            var b = int.Parse(extoex[0].ToString());
            var c = int.Parse(extoex[0].ToString());
            if (b <= 20 && c <= 20 && c <= b)
            {
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static string GetSMSCode()
    {
        const string chars = "0123456789";
        return new string(Enumerable.Repeat(chars, 5)
          .Select(s => s[random1.Next(s.Length)]).ToArray());
    }
    //public static byte[] GenerateCaptcha(string text)
    //{
    //    Bitmap qrCodeImage = GenerateImage(135, 40, text);
    //    byte[] byteArray = ImageToByte(qrCodeImage);
    //    return byteArray;
    //}
    public static string GetRoleOfCode(int code)
    {
        if (code == 10)
        {
            return "VCV Support";
        }
        else if (code == 20)
        {
            return "Your School";
        }
        else if (code == 30)
        {
            return "Your Teacher";
        }
        else
        {
            return "Student";
        }
    }
    //public static byte[] ImageToByte(Image img)
    //{
    //    ImageConverter converter = new ImageConverter();
    //    return (byte[])converter.ConvertTo(img, typeof(byte[]));
    //}
    //public static Bitmap GenerateImage(int Width, int Height, string Phrase)
    //{
    //    Bitmap CaptchaImg = new Bitmap(Width, Height);
    //    Graphics Graphic = Graphics.FromImage(CaptchaImg);
    //    Graphic.SmoothingMode = SmoothingMode.HighSpeed;
    //    //Set height and width of captcha image
    //    Graphic.FillRectangle(new SolidBrush(Color.Transparent), 0, 0, Width, Height);

    //    using (Font font1 = new Font("tahoma", 25, FontStyle.Bold, GraphicsUnit.Point))
    //    {
    //        Rectangle rect1 = new Rectangle(0, 0, Width, Height);

    //        StringFormat stringFormat = new StringFormat();
    //        stringFormat.Alignment = StringAlignment.Center;
    //        stringFormat.LineAlignment = StringAlignment.Near;

    //        // Draw the text and the surrounding rectangle.
    //        Graphic.DrawString(Phrase, font1, Brushes.Gray, rect1, stringFormat);
    //        Graphic.DrawRectangle(Pens.Transparent, rect1);
    //    }
    //    //Graphic.DrawString(Phrase, new Font("Carre", 30),Brushes.Gray,5,5,stringFormat);
    //    Graphic.Flush();
    //    return CaptchaImg;
    //}

    public static string ObjExist(string obj)
    {
        if (obj == null || obj == "")
        {
            return "وجود ندارد";
        }
        else
        {
            return obj;
        }
    }

    public static string ReturnGender(bool sex)
    {
        if (sex)
        {
            return " آقای ";
        }
        else
        {
            return " خانم ";
        }
    }

    public static string GetImageOfRoleDefault(string who)
    {
        return hosturl + "/ProfileImage/" + who + ".png";
    }

    public static string EduFind(int id)
    {
        switch (id)
        {
            case 1: return "اول";
            case 2: return "دوم";
            case 3: return "سوم";
            case 4: return "چهارم";
            case 5: return "پنجم";
            case 6: return "ششم";
            case 7: return "هفتم";
            case 8: return "هشتم";
            case 9: return "نهم";
            case 10: return "دهم";
            case 11: return "یازدهم";
            case 12: return "دوازدهم";
            case 13: return "دیپلم";
            case 14: return "فوق دیپلم";
            case 15: return "لیسانس";

            default:
                {
                    return "notfound";
                }
        }

    }


    public static int DateCompare(string d1, string d2)
    {
        var dl1 = Regex.Split(d1, "/");
        var dl2 = Regex.Split(d2, "/");
        int fdate = int.Parse(dl1[2]);
        int sdate = int.Parse(dl2[2]);
        if (dl1[1] == dl2[1])
        {
            int s = sdate - fdate;
            if (s < 2)
            {
                return 0;
            }
            else if (sdate - fdate < 5)
            {
                return 2;
            }
            else
            {
                return 5;
            }
        }
        else { return 5; }
    }
    public static int PersianTimeDiff2(string d1, string d2)
    {
        try
        {
            DateTime dateTime2 = okdatetime(d2, "00:00");
            DateTime dateTime1 = okdatetime(d1, "00:00");
            var a = (dateTime2 - dateTime1).Days;
            return a;
        }
        catch (Exception)
        {
            return 1;
        }
    }

    public static DateTime okdatetime(string date, string time)
    {
        //var dateTime = Convert.ToDateTime(date);
        var dd = date.Split('/');
        PersianCalendar p = new PersianCalendar();
        DateTime dateTime = p.ToDateTime(int.Parse(dd[0]), int.Parse(dd[1]), int.Parse(dd[2]), 0, 0, 0, 0);

        var timee = time.Split(':');

        return new DateTime(
            dateTime.Year,
            dateTime.Month,
            dateTime.Day,
            int.Parse(timee[0]),
            int.Parse(timee[1]),
            0,
            0,
            dateTime.Kind);
    }

    public static string GetDiffrenceOfDate(string date, string time)
    {
        var dateTimenow = okdatetime(GetDate(), GetTime());
        //DateTime dateTimenow = DateTime.ParseExact(ss, "yyyy/MM/dd-HH:mm", null);
        DateTime dateTime2 = okdatetime(date, time);
        TimeSpan diff = (dateTimenow - dateTime2);

        if (diff.Days > 0)
        {
            if (diff.Days == 1)
            {
                return " دیروز ";
            }
            else if (diff.Days == 2)
            {
                return " پریروز ";
            }
            else
            {
                return GetPersianOfNo(diff.Days) + " روز پیش ";
            }
        }
        else
        {
            if (diff.Hours > 0)
            {
                return GetPersianOfNo(diff.Hours) + " ساعت پیش ";
            }
            else
            {
                if (diff.Minutes > 0)
                {
                    return GetPersianOfNo(diff.Minutes) + " دقیقه پیش ";
                }
                else
                {
                    return "چند لحظه پیش";
                }
            }
        }
    }
    public static TimeSpan PerdianTimeDiff2(string d1, string d2)
    {
        DateTime dateTime2 = DateTime.ParseExact(d2, "yyyy/MM/dd", new CultureInfo("fa-IR"));
        DateTime dateTime1 = DateTime.ParseExact(d1, "yyyy/MM/dd", new CultureInfo("fa-IR"));
        int diffday = (dateTime2 - dateTime1).Days;

        return new TimeSpan(diffday, 0, 0, 0);
    }

    public static string GetPersianOfNo(int num3)
    {
        if (num3 == null || num3 == 0)
        {
            return "صفر";
        }
        string[] yakan = new string[10] { "صفر", "یک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
        string[] dahgan = new string[10] { "", "", "بیست", "سی", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
        string[] dahyek = new string[10] { "ده", "یازده", "دوازده", "سیزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده" };
        string[] sadgan = new string[10] { "", "یکصد", "دویست", "سیصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
        string[] basex = new string[5] { "", "هزار", "میلیون", "میلیارد", "تریلیون" };

        string s = "";
        int d3, d12;
        d12 = num3 % 100;
        d3 = num3 / 100;
        if (d3 != 0)
            s = sadgan[d3] + " و ";
        if ((d12 >= 10) && (d12 <= 19))
        {
            s = s + dahyek[d12 - 10];
        }
        else
        {
            int d2 = d12 / 10;
            if (d2 != 0)
                s = s + dahgan[d2] + " و ";
            int d1 = d12 % 10;
            if (d1 != 0)
                s = s + yakan[d1] + " و ";
            s = s.Substring(0, s.Length - 3);
        };
        return s;
    }

}

