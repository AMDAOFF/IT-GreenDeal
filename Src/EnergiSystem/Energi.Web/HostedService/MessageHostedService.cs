using Energi.Web.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Energi.Web.HostedService
{
    public class MessageHostedService : IHostedService
    {
        private readonly IHubContext<deviceHub> _deviceHub;
        //private readonly IPublishEndpoint _publishEndpoint;
        //public MessageHostedService(IHubContext<deviceHub> deviceHub, IBus publishEndpoint)
        public MessageHostedService(IHubContext<deviceHub> deviceHub)
        {
            _deviceHub = deviceHub;
            //_publishEndpoint = publishEndpoint;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //_deviceHub.Clients.All.SendAsync("updateCurrentTime", "PARAM");
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}