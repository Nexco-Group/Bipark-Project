using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ronisim.Models
{
    public class UserListChat
    {
        public List<UserChat> iuserchat { get; set; }
    }
    public class UserChat
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public string imgUrl { get; set; }        
    }
}
