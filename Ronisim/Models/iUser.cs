using System;
using System.Collections.Generic;

namespace Ronisim.Models
{
    public partial class iUser
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public int? Code { get; set; }
        public string UserName { get; set; }
        public bool Sex { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string PassWord { get; set; }
        public string Salt { get; set; }
        public string JoiningDate { get; set; }
        public string Plate { get; set; }
        public string AccessLevel { get; set; }
        public bool? Verify { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }
    }
}
