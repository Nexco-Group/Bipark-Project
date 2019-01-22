using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ronisim.Models
{
    public class EditInformationViewModel
    {
        [Required(ErrorMessage = "نام فارسی آموزشگاه خود را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام فارسی آموزشگاه وارد شده طولانی میباشد حداکثر 100 کاراکتر فارسی مجاز میباشد")]
        public string schnamefa { get; set; }

        [Required(ErrorMessage = "نام انگلیسی آموزشگاه خود را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام انگلیسی آموزشگاه وارد شده طولانی میباشد حداکثر 100 کاراکتر انگلیسی مجاز میباشد")]
        public string schnameen { get; set; }

        [Required(ErrorMessage = "سمت خود را وارد کنید")]
        [StringLength(20, ErrorMessage = "سمت وارد شده طولانی میباشد حداکثر 20 کاراکتر فارسی مجاز میباشد")]
        public string position { get; set; }

        [Required(ErrorMessage = "نام فارسی خود را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام فارسی وارد شده طولانی میباشد حداکثر 100 کاراکتر مجاز میباشد")]
        public string cfnamefa { get; set; }

        [Required(ErrorMessage = "نام خانوادگی فارسی خود را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام خانوادگی فارسی وارد شده طولانی میباشد حداکثر 100 کاراکتر انگلیسی مجاز میباشد")]
        public string elnamefa { get; set; }

        [Required(ErrorMessage = "نام انگلیسی خود را وارد کنید")]
        [RegularExpression(@"^([A-Za-z&]\S*\s*)+$", ErrorMessage = "فرمت نام انگلیسی وارد شده درست نمی باشد فقط کاراکتر های انگلیسی")]
        [StringLength(100, ErrorMessage = "نام انگلیسی وارد شده طولانی میباشد حداکثر 100 کاراکتر انگلیسی مجاز میباشد")]
        public string efnameen { get; set; }

        [Required(ErrorMessage = "نام خانوادگی انگلیسی خود را وارد کنید")]
        [RegularExpression(@"^([A-Za-z&]\S*\s*)+$", ErrorMessage = "فرمت نام انگلیسی وارد شده درست نمی باشد فقط کاراکتر های انگلیسی")]
        [StringLength(100, ErrorMessage = "نام خانوادگی انگلیسی وارد شده طولانی میباشد حداکثر 100 کاراکتر انگلیسی مجاز میباشد")]
        public string elnameen { get; set; }

        //[Required(ErrorMessage = "نام پدر خود را وارد کنید")]
        //[StringLength(100, ErrorMessage = "نام پدر وارد شده طولانی میباشد حداکثر 100 کاراکتر مجاز میباشد")]
        //public string efaname { get; set; }

        [Required(ErrorMessage = "سال تولد خود را وارد کنید")]
        [StringLength(5, ErrorMessage = "سال تولد وارد شده طولانی میباشد حداکثر 4 کاراکتر مجاز میباشد")]
        public string birthyinput { get; set; }

        [Required(ErrorMessage = "ماه تولد خود را وارد کنید")]
        [StringLength(3, ErrorMessage = "ماه تولد وارد شده طولانی میباشد حداکثر 2 کاراکتر مجاز میباشد")]
        public string birthminput { get; set; }

        [Required(ErrorMessage = "روز تولد خود را وارد کنید")]
        [StringLength(3, ErrorMessage = "روز تولد وارد شده طولانی میباشد حداکثر 2 کاراکتر مجاز میباشد")]
        public string birthdinput { get; set; }

        //public string nationcode { get; set; }

        //[Required(ErrorMessage = "مقطع تحصیلی خود را وارد کنید")]
        //[StringLength(2, ErrorMessage = "تحصیلات وارد شده مجاز نمی باشد")]
        //public string eduinput { get; set; }

        [StringLength(300, ErrorMessage = "ایمیل وارد شده مجاز نمی باشد")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "فرمت ایمیل وارد شده صحیح نمی باشد")]
        public string eemail { get; set; }

        [Required(ErrorMessage = "تلفن تماس آموزشگاه را وارد کنید")]
        [StringLength(20, ErrorMessage = "تلفن تماس آموزشگاه وارد شده طولانی میباشد حداکثر 20 کاراکتر مجاز میباشد")]
        public string schphone { get; set; }

        [Required(ErrorMessage = "آدرس آموزشگاه را وارد کنید")]
        [StringLength(100, ErrorMessage = "آدرس آموزشگاه وارد شده طولانی میباشد حداکثر 100 کاراکتر فارسی مجاز میباشد")]
        public string address { get; set; }

        [Required(ErrorMessage = "کد پستی آموزشگاه را وارد کنید")]
        [StringLength(20, ErrorMessage = "کد پستی وارد شده طولانی میباشد حداکثر 20 کاراکتر فارسی مجاز میباشد")]
        public string zipcode { get; set; }
    }
}
