using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAD_Web_API.Models
{
    public class Setting
    {
        public int SettingID { get; set; }
        public string UserName { get; set; }
        public string HideRegions { get; set; }
        public string TSOID { get; set; }
        public string UserSettings { get; set; }
        public string EmailID { get; set; }
    }
}