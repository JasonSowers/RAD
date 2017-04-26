using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAD_Web_API.Models
{
    public class Region
    {
        public int RegionID { get; set; }
        public string RegionName { get; set; }
        public string Status { get; set; }
        public DateTime RegionDate { get; set; }
        public int TimeUntilDown { get; set; }
        public int EstimatedTimeDown { get; set; }
        public string Comment { get; set; }
    }
}