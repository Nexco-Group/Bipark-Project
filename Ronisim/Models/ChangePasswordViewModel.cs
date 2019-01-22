using System.ComponentModel.DataAnnotations;

namespace Ronisim.Models
{

    public class EditInfoOrder
    {
        [Required(ErrorMessage = "شماره موبایل خود را وارد کنید")]
        //[StringLength(14, ErrorMessage = "شماره موبایل وارد شده مورد تایید نیست")]
        [RegularExpression(@"09\d{9}", ErrorMessage = "شماره موبایل وارد شده مورد تایید نیست")]
        public string mobile { get; set; }
        [Required(ErrorMessage = "تلفن تماس را وارد کنید")]
        [StringLength(20, ErrorMessage = "تلفن تماس وارد شده طولانی میباشد حداکثر 20 کاراکتر مجاز میباشد")]
        public string schphone { get; set; }

        [Required(ErrorMessage = "آدرس را وارد کنید")]
        [StringLength(100, ErrorMessage = "آدرس وارد شده طولانی میباشد حداکثر 100 کاراکتر فارسی مجاز میباشد")]
        public string address { get; set; }

        [Required(ErrorMessage = "کد پستی را وارد کنید")]
        [StringLength(20, ErrorMessage = "کد پستی وارد شده طولانی میباشد حداکثر 20 کاراکتر فارسی مجاز میباشد")]
        public string zipcode { get; set; }
    }
    public class ChangePasswordViewModel
    {

        [Required(ErrorMessage = "رمز عبور فعلی خود را وارد کنید")]
        [StringLength(20, ErrorMessage = "حداقل تعداد کاراکتر برای رمز عبور فعلی {2} می باشد", MinimumLength = 6)]
        [RegularExpression("^[a-zA-Z0-9_.-]*$", ErrorMessage = "رمز عبور فعلی باید به انگلیسی باشد")]
        [DataType(DataType.Password)]
        public string cp { get; set; }

        [Required(ErrorMessage = "رمز عبور جدید خود را وارد کنید")]
        [StringLength(20, ErrorMessage = "حداقل تعداد کاراکتر برای رمز عبور جدید {2} می باشد", MinimumLength = 6)]
        [RegularExpression("^[a-zA-Z0-9_.-]*$", ErrorMessage = "رمز عبور جدید باید به انگلیسی باشد")]
        [DataType(DataType.Password)]
        public string np { get; set; }

        [Required(ErrorMessage = "تکرار رمز عبور خود را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("np", ErrorMessage = "رمز عبور با تکرار آن مطابقت ندارد")]
        public string renp { get; set; }

    }

    public class editcode
    {
        [Required(ErrorMessage = "زبان آموز را انتخاب کنید")]
        public int who { get; set; }
        public int? val { get; set; }
    }

    public class ChangePasswordStdViewModel
    {
        [Required(ErrorMessage = "زبان آموز را انتخاب کنید")]
        public int who { get; set; }
        [Required(ErrorMessage = "رمز عبور جدید خود را وارد کنید")]
        [StringLength(20, ErrorMessage = "حداقل تعداد کاراکتر برای رمز عبور جدید {2} می باشد", MinimumLength = 6)]
        [RegularExpression("^[a-zA-Z0-9_.-]*$", ErrorMessage = "رمز عبور جدید باید به انگلیسی باشد")]
        [DataType(DataType.Password)]
        public string np { get; set; }

        [Required(ErrorMessage = "تکرار رمز عبور خود را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("np", ErrorMessage = "رمز عبور با تکرار آن مطابقت ندارد")]
        public string renp { get; set; }

    }
    public class ChangeSMS
    {
        [Required(ErrorMessage = "Enter SMS Api")]
        public string smsapi { get; set; }
        [Required(ErrorMessage = "Enter SMS Number")]
        public string smsno { get; set; }
        [Required(ErrorMessage = "Enter SMS UserName")]
        public string smsuser { get; set; }
        [Required(ErrorMessage = "Enter SMS PassWord")]
        public string smspass { get; set; }

    }

}
