using Energi.Service.MessageService.DTO;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static MessageBroker.Contracts.Contracts;

namespace Energi.Service.MessageService
{
    public class MessageService : IMessageService
    {
        //private readonly IPublishEndpoint _publishEndpoint;

        IBusControl _busControl;

        public MessageService()
        {
            //_publishEndpoint = publishEndpoint;
            SetupRabbitMQ();
        }



        public async Task SendMessage(PublishMessageDTO message)
        {
            await _busControl.Publish(new RoomUpdate(message.RoomNr, message.PeopleCount, message.TimeStamp));
        }

        public async Task StopListener()
        {
            await _busControl.StopAsync();
        }


        private async Task SetupRabbitMQ()
        {
            _busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint("RoomUpdate", e =>
                {
                    e.Consumer<ReceivedMessage>();
                });
            });

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await _busControl.StartAsync(source.Token);
        }

        class ReceivedMessage : IConsumer<RoomUpdate>
        {
            public async Task Consume(ConsumeContext<RoomUpdate> context)
            {
                Console.WriteLine("Order Submitted: {0}", context.Message.ClassroomNr);
            }
        }
    }
}
