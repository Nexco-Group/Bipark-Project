using System.ComponentModel.DataAnnotations;

namespace Ronisim.Models
{
    public class CompleteStep4ViewModel
    {
        [Required(ErrorMessage = "نام فارسی آموزشگاه خود را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام فارسی آموزشگاه وارد شده طولانی میباشد حداکثر 100 کاراکتر فارسی مجاز میباشد")]
        public string schnamefa { get; set; }

        [Required(ErrorMessage = "نام انگلیسی آموزشگاه خود را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام انگلیسی آموزشگاه وارد شده طولانی میباشد حداکثر 100 کاراکتر انگلیسی مجاز میباشد")]
        public string schnameen { get; set; }

        [Required(ErrorMessage = "تلفن تماس آموزشگاه را وارد کنید")]
        [StringLength(20, ErrorMessage = "تلفن تماس آموزشگاه وارد شده طولانی میباشد حداکثر 20 کاراکتر مجاز میباشد")]
        public string schphone { get; set; }

        [Required(ErrorMessage = "سمت خود را وارد کنید")]
        [StringLength(20, ErrorMessage = "سمت وارد شده طولانی میباشد حداکثر 20 کاراکتر فارسی مجاز میباشد")]
        public string position { get; set; }

        [Required(ErrorMessage = "آدرس آموزشگاه را وارد کنید")]
        [StringLength(100, ErrorMessage = "آدرس آموزشگاه وارد شده طولانی میباشد حداکثر 100 کاراکتر فارسی مجاز میباشد")]
        public string address { get; set; }

        [Required(ErrorMessage = "کد پستی آموزشگاه را وارد کنید")]
        [StringLength(20, ErrorMessage = "کد پستی وارد شده طولانی میباشد حداکثر 20 کاراکتر فارسی مجاز میباشد")]
        public string zipcode { get; set; }

        [Required(ErrorMessage = "لطفا شهر خود را انتخاب کنید")]
        public string combocity { get; set; }
    }
}
