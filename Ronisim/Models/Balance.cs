using System;
using System.Collections.Generic;

namespace Ronisim.Models
{
    public partial class Balance
    {
        public int Id { get; set; }
        public string Who { get; set; }
        public int? WhoId { get; set; }
        public int? Amount { get; set; }
        public int? RequestId { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public bool? Consider { get; set; }
        public bool? Active { get; set; }
    }
}
