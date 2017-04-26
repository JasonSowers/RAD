using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RAD_Web_API.Models
{
    public class RAD_Web_APIContext : DbContext
    {
        public RAD_Web_APIContext()
            : base("name=RAD_Web_APIContext")
        {
        }

        public System.Data.Entity.DbSet<RAD_Web_API.Models.Region> Regions { get; set; }

        public System.Data.Entity.DbSet<RAD_Web_API.Models.CLStatu> CLStatus { get; set; }

        public System.Data.Entity.DbSet<RAD_Web_API.Models.Setting> Settings { get; set; }

        public System.Data.Entity.DbSet<RAD_Web_API.Models.History> Histories { get; set; }

        public System.Data.Entity.DbSet<RAD_Web_API.Models.IACError> IACErrors { get; set; }
    }
}
