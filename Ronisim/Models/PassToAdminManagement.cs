using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ronisim.Models
{
    public class PassToAdminManagement
    {
        public List<iUser> iiUsers { get; set; }

        public List<News> iActiveNews { get; set; }
        public List<News> iUnActiveNews { get; set; }
        public List<string> iNotification { get; set; }
    }
}
