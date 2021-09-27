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
        public MessageHostedService(IHubContext<deviceHub> deviceHub)
        {
            _deviceHub = deviceHub;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}