using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ronisim.Models
{
    public class AddNews
    {
        [Required(ErrorMessage = "تیتر اطلاعیه را وارد کنید")]
        [StringLength(500, ErrorMessage = "تیتر اطلاعیه وارد شده مورد تایید نیست")]
        public string Newstitle { get; set; }
        public string Newsurl { get; set; }
        public IFormFile Newsimage { get; set; }
        public bool iUserchecked { get; set; }
        public bool ruserchecked { get; set; }
    }

    public class EditNews
    {
        [Required(ErrorMessage = "مشکل رخ داده")]
        public int Id { get; set; }
        [Required(ErrorMessage = "تیتر اطلاعیه را وارد کنید")]
        [StringLength(100, ErrorMessage = "تیتر اطلاعیه وارد شده مورد تایید نیست")]
        public string Newstitle { get; set; }
        public bool schoolchecked { get; set; }
        public bool teacherchecked { get; set; }
    }

    public class InsertSerial
    {
        [Required(ErrorMessage = "سطح را وارد کنید")]
        public string level { get; set; }
        public IFormFile excel { get; set; }
    }

    public class ChangeClass
    {
        [Required(ErrorMessage = "دوباره تلاش کنید")]
        public int iclsId { get; set; }
        [Required(ErrorMessage = "کد کلاس را وارد کنید")]
        [StringLength(10, ErrorMessage = "کد کلاس وارد شده طولانی می باشد. حداکثر 10 کاراکتر مجاز است")]
        public string iclscode { get; set; }
        [Required(ErrorMessage = "سطح کلاس را وارد کنید")]
        [StringLength(5, ErrorMessage = "سطح کلاس وارد شده مورد تایید نیست")]
        public string iclslevel1 { get; set; }
        [Required(ErrorMessage = "تعداد تمرین های سطح کلاس را وارد کنید")]
        [StringLength(5, ErrorMessage = "تعداد تمرین های سطح کلاس وارد شده مورد تایید نیست ")]
        public string iclsles1 { get; set; }
        public string iclslevel2 { get; set; }
        public string iclsles2 { get; set; }
        [Required(ErrorMessage = "مدرس کلاس را انتخاب کنید")]
        public int iclstch { get; set; }
        [Required(ErrorMessage = "تاریخ شروع کلاس را وارد کنید")]
        public string iclsstartDate { get; set; }
        [Required(ErrorMessage = "تاریخ پایان کلاس را وارد کنید")]
        public string iclsfinalDate { get; set; }
        public int? iclscapacity { get; set; }
    }
}
