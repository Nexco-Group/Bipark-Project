using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ronisim.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "شماره موبایل خود را وارد کنید")]
        [RegularExpression(@"09\d{9}", ErrorMessage = "شماره موبایل وارد شده مورد تایید نیست")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "رمز عبور خود را وارد کنید")]
        [StringLength(20, ErrorMessage = "حداقل تعداد کاراکتر برای رمز عبور {2} می باشد", MinimumLength = 6)]
        [RegularExpression("^[a-zA-Z0-9_.-]*$", ErrorMessage = "رمز عبور باید به انگلیسی باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Captcha { get; set; }

        [Required]
        public bool RememberMe { get; set; }
    }
}
