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
    public class MessageHostedService : IHostedService, IConsumer<RoomUpdate>
    {
        private readonly IHubContext<deviceHub> _deviceHub;
        private readonly IMessageService _messageService;
        private readonly MessageBusSettings _settings;
        private readonly MessageBusSettings _busSettings;

        //private readonly IPublishEndpoint _publishEndpoint;
        //public MessageHostedService(IHubContext<deviceHub> deviceHub, IBus publishEndpoint)


        public MessageHostedService(IHubContext<deviceHub> deviceHub, IMessageService messageService, IConfiguration Configuration)
        {
            _settings = Configuration.GetSection(nameof(MessageBusSettings)).Get<MessageBusSettings>();
            _deviceHub = deviceHub;
            _messageService = messageService;             
            //this._busSettings = busSettings;
            //_publishEndpoint = publishEndpoint;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _messageService.Initialize(_settings, Consume);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _messageService.StopListener();
        }

        public async Task Consume(ConsumeContext<RoomUpdate> context)
        {
            Console.WriteLine("Classroom update: {0}", context.Message.RoomNr);
        }

        //class ReceivedMessage : IConsumer<RoomUpdate>
        //{
        //    public async Task Consume(ConsumeContext<RoomUpdate> context)
        //    {
        //        Console.WriteLine("Classroom update: {0}", context.Message.RoomNr);
        //    }
        //}
    }
}
