namespace RAD_Web_API.Migrations
{
    using RAD_Web_API.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RAD_Web_API.Models.RAD_Web_APIContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(RAD_Web_API.Models.RAD_Web_APIContext context)
        {
            context.CLStatus.Add(new CLStatu { Status = "Open", LastUpdate = DateTime.Now });

            context.Regions.AddOrUpdate(x => x.RegionID,
                new Region()
                {
                    RegionID = 1,
                    RegionName = "LP02",
                    Status = "UP",
                    RegionDate = new DateTime(2015, 9, 15),
                    TimeUntilDown = 0,
                    EstimatedTimeDown = 0,
                    Comment = ""
                },
                new Region()
                {
                    RegionID = 2,
                    RegionName = "LP03",
                    Status = "UP",
                    RegionDate = new DateTime(2015, 9, 15),
                    TimeUntilDown = 0,
                    EstimatedTimeDown = 0,
                    Comment = ""
                },
                new Region()
                {
                    RegionID = 3,
                    RegionName = "LP04",
                    Status = "UP",
                    RegionDate = new DateTime(2015, 9, 15),
                    TimeUntilDown = 0,
                    EstimatedTimeDown = 0,
                    Comment = ""
                },
                new Region()
                {
                    RegionID = 4,
                    RegionName = "LP22",
                    Status = "UP",
                    RegionDate = new DateTime(2015, 9, 15),
                    TimeUntilDown = 0,
                    EstimatedTimeDown = 0,
                    Comment = ""
                },
                new Region()
                {
                    RegionID = 5,
                    RegionName = "LP23",
                    Status = "UP",
                    RegionDate = new DateTime(2015, 9, 15),
                    TimeUntilDown = 0,
                    EstimatedTimeDown = 0,
                    Comment = ""
                },
                new Region()
                {
                    RegionID = 6,
                    RegionName = "LP24",
                    Status = "UP",
                    RegionDate = new DateTime(2015, 9, 15),
                    TimeUntilDown = 0,
                    EstimatedTimeDown = 0,
                    Comment = ""
                },
                new Region()
                {
                    RegionID = 7,
                    RegionName = "LP42",
                    Status = "UP",
                    RegionDate = new DateTime(2015, 9, 15),
                    TimeUntilDown = 0,
                    EstimatedTimeDown = 0,
                    Comment = ""
                },
                new Region()
                {
                    RegionID = 8,
                    RegionName = "LP43",
                    Status = "UP",
                    RegionDate = new DateTime(2015, 9, 15),
                    TimeUntilDown = 0,
                    EstimatedTimeDown = 0,
                    Comment = ""
                },
                new Region()
                {
                    RegionID = 9,
                    RegionName = "LP44",
                    Status = "UP",
                    RegionDate = new DateTime(2015, 9, 15),
                    TimeUntilDown = 0,
                    EstimatedTimeDown = 0,
                    Comment = ""
                },
                new Region()
                {
                    RegionID = 10,
                    RegionName = "LC03",
                    Status = "UP",
                    RegionDate = new DateTime(2015, 9, 15),
                    TimeUntilDown = 0,
                    EstimatedTimeDown = 0,
                    Comment = ""
                },
                new Region()
                {
                    RegionID = 11,
                    RegionName = "LC04",
                    Status = "UP",
                    RegionDate = new DateTime(2015, 9, 15),
                    TimeUntilDown = 0,
                    EstimatedTimeDown = 0,
                    Comment = ""
                }
                );
        }
    }
}
