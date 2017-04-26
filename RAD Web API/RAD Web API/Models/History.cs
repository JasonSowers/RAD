using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAD_Web_API.Models
{
    public class History
    {
        public int HistoryID { get; set; }
        public string RegionName { get; set; }
        public string UserID { get; set; }
        public string Action { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
    }
}