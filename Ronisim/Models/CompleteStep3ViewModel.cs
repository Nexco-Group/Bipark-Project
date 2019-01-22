using System.ComponentModel.DataAnnotations;

namespace Ronisim.Models
{
    public class CompleteStep3ViewModel
    {
        [Required(ErrorMessage = "نام انگلیسی خود را وارد کنید")]
        [RegularExpression(@"^([A-Za-z&]\S*\s*)+$", ErrorMessage = "فرمت نام انگلیسی وارد شده درست نمی باشد فقط کاراکتر های انگلیسی")]
        [StringLength(100, ErrorMessage = "نام انگلیسی وارد شده طولانی میباشد حداکثر 100 کاراکتر انگلیسی مجاز میباشد")]
        public string FnameEn { get; set; }

        [Required(ErrorMessage = "نام خانوادگی انگلیسی خود را وارد کنید")]
        [RegularExpression(@"^([A-Za-z&]\S*\s*)+$", ErrorMessage = "فرمت نام انگلیسی وارد شده درست نمی باشد فقط کاراکتر های انگلیسی")]
        [StringLength(100, ErrorMessage = "نام خانوادگی انگلیسی وارد شده طولانی میباشد حداکثر 100 کاراکتر انگلیسی مجاز میباشد")]
        public string LnameEn { get; set; }

        [Required(ErrorMessage = "سال تولد خود را وارد کنید")]
        [StringLength(5, ErrorMessage = "سال تولد وارد شده طولانی میباشد حداکثر 4 کاراکتر مجاز میباشد")]
        public string BirthYear { get; set; }

        [Required(ErrorMessage = "ماه تولد خود را وارد کنید")]
        [StringLength(3, ErrorMessage = "ماه تولد وارد شده طولانی میباشد حداکثر 2 کاراکتر مجاز میباشد")]
        public string BirthMonth { get; set; }

        [Required(ErrorMessage = "روز تولد خود را وارد کنید")]
        [StringLength(3, ErrorMessage = "روز تولد وارد شده طولانی میباشد حداکثر 2 کاراکتر مجاز میباشد")]
        public string BirthDay { get; set; }

        [Required(ErrorMessage = "کد ملی خود را وارد کنید")]
        public string NationCode { get; set; }

        [StringLength(2, ErrorMessage = "تحصیلات وارد شده مجاز نمی باشد")]
        public string Education { get; set; }

        [StringLength(300, ErrorMessage = "ایمیل وارد شده مجاز نمی باشد")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "فرمت ایمیل وارد شده صحیح نمی باشد")]
        public string Email { get; set; }

    }
}
