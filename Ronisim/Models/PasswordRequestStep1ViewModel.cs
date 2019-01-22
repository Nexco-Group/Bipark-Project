using System.ComponentModel.DataAnnotations;

namespace Ronisim.Models
{
    public class PasswordRequestStep1ViewModel
    {
        [Required(ErrorMessage = "شماره موبایل خود را وارد کنید")]
        [RegularExpression(@"09\d{9}", ErrorMessage = "شماره موبایل وارد شده مورد تایید نیست")]
        public string Mobile { get; set; }

        //[Required(ErrorMessage = "تصویر امنیتی را وارد کنید")]
        //[StringLength(5, ErrorMessage = "تصویر امنیتی مورد تایید نیست")]
        public string Captcha { get; set; }
    }
}
