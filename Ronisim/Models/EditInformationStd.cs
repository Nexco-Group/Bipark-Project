using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ronisim.Models
{
    public class EditInformationStd
    {
        [Required(ErrorMessage = "مشکلی پیش آمده")]
        public int Id { get; set; }

        [Required(ErrorMessage = "نام را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام وارد شده طولانی میباشد حداکثر 100 کاراکتر مجاز میباشد")]
        public string rfname { get; set; }

        [Required(ErrorMessage = "نام خانوادگی را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام وارد شده طولانی میباشد حداکثر 100 کاراکتر مجاز میباشد")]
        public string rlname { get; set; }

        [Required(ErrorMessage = "کد کاربر را وارد کنید")]
        public int rcode { get; set; }

        [Required(ErrorMessage = "شماره موبایل را وارد کنید")]
        [RegularExpression(@"09\d{9}", ErrorMessage = "شماره موبایل وارد شده مورد تایید نیست")]
        public string rmobile { get; set; }

        public string rpass { get; set; }        
    }
}
