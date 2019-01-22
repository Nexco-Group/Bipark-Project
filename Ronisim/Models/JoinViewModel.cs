using System.ComponentModel.DataAnnotations;

namespace Ronisim.Models
{
    public class JoinViewModel
    {
        //checkchange
        [Required(ErrorMessage = "نام خود را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام وارد شده طولانی میباشد حداکثر 100 کاراکتر مجاز میباشد")]
        public string FnameFa { get; set; }

        [Required(ErrorMessage = "نام خانوادگی خود را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام خانوادگی وارد شده طولانی میباشد حداکثر 100 کاراکتر مجاز میباشد")]
        public string LnameFa { get; set; }

        [Required(ErrorMessage = "شماره موبایل خود را وارد کنید")]
        //[StringLength(14, ErrorMessage = "شماره موبایل وارد شده مورد تایید نیست")]
        [RegularExpression(@"09\d{9}", ErrorMessage = "شماره موبایل وارد شده مورد تایید نیست")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "شماره پلاک خود را وارد کنید")]
        public string Plate { get; set; }

        [Required(ErrorMessage = "گذرواژه خود را وارد کنید")]
        [StringLength(100, ErrorMessage = "حداقل تعداد کاراکتر برای گذرواژه {2} و حداکثر {1} می باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "تکرار گذرواژه خود را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "گذرواژه وارده با تکرار آن مطابقت ندارد")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "جنسیت خود را وارد کنید")]
        public bool GenderInput { get; set; }      
    }
}
