using System;
using System.Collections.Generic;

namespace Ronisim.Models
{
    public partial class News
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public bool? iUser { get; set; }
        public bool? Ruser { get; set; }
        public string Date { get; set; }
        public bool? Active { get; set; }
    }
}
