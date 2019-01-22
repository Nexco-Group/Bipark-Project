using System.ComponentModel.DataAnnotations;

namespace Ronisim.Models
{
    public class PasswordRequestStep2ViewModel
    {
        [Required(ErrorMessage = "رمز عبور خود را وارد کنید")]
        [StringLength(20, ErrorMessage = "حداقل تعداد کاراکتر برای رمز عبور {2} می باشد", MinimumLength = 6)]
        [RegularExpression("^[a-zA-Z0-9_.-]*$", ErrorMessage = "رمز عبور باید به انگلیسی باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "تکرار رمز عبور خود را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "رمز عبور با تکرار آن مطابقت ندارد")]
        public string ConfirmPassword { get; set; }
    }
}
