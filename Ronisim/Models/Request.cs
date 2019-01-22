using System;
using System.Collections.Generic;

namespace Ronisim.Models
{
    public partial class Request
    {
        public int Id { get; set; }
        public int? WhoId { get; set; }
        public int PackId { get; set; }
        public string RequestCode { get; set; }
        public int? TotalPrice { get; set; }
        public string OrderDate { get; set; }
        public string OrderTime { get; set; }
        public string FromDate { get; set; }
        public string FromTime { get; set; }
        public string ToDate { get; set; }
        public string ToTime { get; set; }
        public int? Status { get; set; }
        public bool? Active { get; set; }
        public string Note { get; set; }
        public bool? Deleted { get; set; }
    }
}
