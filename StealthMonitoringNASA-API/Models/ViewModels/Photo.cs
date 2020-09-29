using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StealthMonitoringNASA_API.Models.ViewModels
{
    public class Photo
    {
        public int id { get; set; }
        public int sol { get; set; }
        public Camera camera { get; set; }
        public string img_src { get; set; }
        public DateTime earth_date { get; set; }
        public Rover rover { get; set; }
    }
}
