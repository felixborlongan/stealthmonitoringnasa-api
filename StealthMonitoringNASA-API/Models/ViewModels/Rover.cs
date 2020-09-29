using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StealthMonitoringNASA_API.Models.ViewModels
{
    public class Rover
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime landing_date { get; set; }
        public DateTime launch_date { get; set; }
        public string status { get; set; }
    }
}
