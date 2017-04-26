using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.SignalR;
using System.Timers;

namespace RAD_Web_API
{
    public class RADHub : Hub
    {
        public static List<ShutDownInfo> shutDownRequestList = new List<ShutDownInfo>();

        // Initiates a Shutdown request
        public void RequestShutDown(string requester, string regionName, int timeUntilDown)
        {
            // add this shutdown request to list
            ShutDownInfo shutDownInfo = new ShutDownInfo(Context.ConnectionId, requester, regionName, 
                                                    timeUntilDown, DateTime.Now.AddMinutes(timeUntilDown));
            shutDownRequestList.Add(shutDownInfo);

            Clients.Others.requestShutDown(requester, regionName);

            // start a timer, then if no one complains after timeUntilDown has elapsed, 
            // send a message to requester to go for it
            shutDownInfo.Timer = new Timer(60000);
            shutDownInfo.Timer.Elapsed += OnTimedEvent;
            shutDownInfo.Timer.Start();
        }

        // Sent to cancel a Shutdown request
        public void RespondNoShutDown(string requester, string responder, string regionName, string comment)
        {
            //get the connection ID of the requester
            ShutDownInfo shutDownInfo =
                shutDownRequestList.Where(r => r.RegionName == regionName &&
                                          r.UserName == requester).LastOrDefault();

            if (shutDownInfo != null)
            {
                Clients.Client(shutDownInfo.ConnectionId).respondNoShutDown(responder, regionName, comment);
            }

            shutDownRequestList.Remove(shutDownInfo);
        }

        // Notification that the Shutdown has been canceled
        public void CancelShutDown(string requester, string regionName)
        {
            Clients.Others.cancelShutDown(requester, regionName);
        }

        public void CancelPending(string regionName)
        {
            // find it in the list
            ShutDownInfo shutDownInfo =
                shutDownRequestList.Where(r => r.RegionName == regionName).LastOrDefault();

            // if null, can't really figure out what to do here
            if(shutDownInfo != null )
            {
                // find out if the cancel requester is original shutdown requester or someone else
                if (Context.ConnectionId == shutDownInfo.ConnectionId)
                {
                    Clients.Others.cancelShutDown(shutDownInfo.UserName, regionName);
                    shutDownRequestList.Remove(shutDownInfo);
                }
                else
                {
                    DateTime dt = DateTime.Now;

                    TimeSpan mins = new TimeSpan(shutDownInfo.ApprTimeDown.Ticks - dt.Ticks);

                    Clients.Client(Context.ConnectionId).requestPendingShutDown(shutDownInfo.UserName, 
                                                                    regionName, mins.Minutes, mins.Seconds);
                }
            }
        }

        // Tells all client that a Region status has changed
        public void StatusChanged(string regionName, string status)
        {
            Clients.All.statusChanged(regionName, status);
        }

        // Tells all client that a Region status has changed
        public void PendingShutdowns()
        {
            if (shutDownRequestList.Count > 0)
            {
                string regions = string.Empty;
                for (int i = 0; i < shutDownRequestList.Count; i++)
                {
                    regions += shutDownRequestList[i].RegionName + " ";
                }

                Clients.Client(Context.ConnectionId).pendingShutdowns(regions);
            }
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Timer tt = source as Timer;
            ShutDownInfo shutDownInfo = shutDownRequestList.Where(r => r.Timer == tt).LastOrDefault();

            // Send the requester a "go ahead" message
            if (shutDownInfo != null && ++(shutDownInfo.Counter) >= shutDownInfo.TimeUntilDown)
            {
                tt.Stop();
                tt.Dispose();

                Clients.AllExcept(shutDownInfo.ConnectionId).statusChanged(shutDownInfo.RegionName, null);
                Clients.Client(shutDownInfo.ConnectionId).proceedWithShutDown(shutDownInfo.RegionName, shutDownInfo.TimeUntilDown);

                shutDownRequestList.Remove(shutDownInfo);
            }
        }
    }

    public class ShutDownInfo
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public string RegionName { get; set; }

        public Timer Timer { get; set; }
        public int Counter { get; set; }
        public int TimeUntilDown { get; set; }
        public DateTime ApprTimeDown { get; set; }

        public ShutDownInfo(string connectionID, string userName, string regionName, int timeUntilDown, DateTime approxTimeDown)
        {
            ConnectionId = connectionID;
            UserName = userName;
            RegionName = regionName;

            Counter = 0;
            TimeUntilDown = timeUntilDown;
            ApprTimeDown = approxTimeDown;
        }
    }
}