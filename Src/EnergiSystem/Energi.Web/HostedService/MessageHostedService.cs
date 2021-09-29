using Energi.Service.MessageService;
using Energi.Web.Hubs;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static MessageBroker.Contracts.Contracts;

namespace Energi.Web.HostedService
{
    public class MessageHostedService : IHostedService, IConsumer<MessageAsString>
    {
        private readonly IHubContext<deviceHub> _deviceHub;
        private readonly IMessageService _messageService;
        private readonly MessageBusSettings _settings;        

        public MessageHostedService(IHubContext<deviceHub> deviceHub, IMessageService messageService, IConfiguration Configuration)
        {
            _settings = Configuration.GetSection(nameof(MessageBusSettings)).Get<MessageBusSettings>();
            _deviceHub = deviceHub;
            _messageService = messageService;             
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _messageService.Initialize(_settings, Consume);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _messageService.StopListener();
        }
        public async Task Consume(ConsumeContext<MessageAsString> context)
        {
            List<string> stringList = context.Message.ToString().Split(';').ToList();

            Console.WriteLine("Classroom update: {0}", context.Message);
        }
    }
}
