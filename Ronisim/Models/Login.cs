using System;
using System.Collections.Generic;

namespace Ronisim.Models
{
    public partial class Login
    {
        public int Id { get; set; }
        public string Who { get; set; }
        public int? WhoId { get; set; }
        public string TrackDate { get; set; }
        public string TrackTime { get; set; }
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string Type { get; set; }
    }
}
