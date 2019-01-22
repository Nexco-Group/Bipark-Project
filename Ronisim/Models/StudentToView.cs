using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ronisim.Models
{
    public class StudentToView
    {
        public int Id { get; set; }
        public string FnameFa { get; set; }
        public string LnameFa { get; set; }
        public string FnameEn { get; set; }
        public string LnameEn { get; set; }
        public string FatherNameFa { get; set; }
        public bool Sex { get; set; }
        public string BirthDay { get; set; }
        public string NationCode { get; set; }
        public string Education { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string PassWord { get; set; }
        public string Salt { get; set; }
        public string ImagePath { get; set; }
        public string JoiningDate { get; set; }
        public string RegState { get; set; }
        public bool? Active { get; set; }
        public bool HaveActiveClass { get; set; }
    }

}
