using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Web.Http;

namespace RAD_Web_API.Controllers
{
    public abstract class ApiControllerWithHub<THub> : ApiController
        where THub : IHub
    {
        readonly Lazy<IHubContext> _hub = new Lazy<IHubContext>(
              () => GlobalHost.ConnectionManager.GetHubContext<THub>()
          );

        protected IHubContext Hub
        {
            get { return _hub.Value; }
        }
    }
}