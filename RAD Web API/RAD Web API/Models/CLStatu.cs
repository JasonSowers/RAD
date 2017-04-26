using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAD_Web_API.Models
{
    public class CLStatu
    {
        public int CLStatuID { get; set; }
        public string Status { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}