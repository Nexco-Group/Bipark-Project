using System;
using System.Collections.Generic;

namespace Ronisim.Models
{
    public partial class Admins
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string PassWord { get; set; }
        public string Salt { get; set; }
        public string JoiningDate { get; set; }
        public string AccessLevel { get; set; }
        public bool? Active { get; set; }
    }
}
