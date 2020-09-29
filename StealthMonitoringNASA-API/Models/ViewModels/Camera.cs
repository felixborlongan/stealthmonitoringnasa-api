using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StealthMonitoringNASA_API.Models.ViewModels
{
    public class Camera
    {
        public string id { get; set; }
        public string name { get; set; }
        public int rover_id { get; set; }
        public string full_name { get; set; }
    }
}
