using System;
using System.Collections.Generic;

namespace Ronisim.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price1 { get; set; }
        public int Price2 { get; set; }
        public string Notes { get; set; }
        public string CreatDate { get; set; }
        public bool? Active { get; set; }
        public bool? Deleted { get; set; }

    }
}
