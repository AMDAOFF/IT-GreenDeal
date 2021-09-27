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
        private MessageBusSettings _setting;
        IBusControl _busControl;

        public async Task SendMessage(PublishMessageDTO message)
        {
            await _busControl.Publish(new RoomUpdate(message.RoomNr, message.PeopleCount, message.TimeStamp));
        }

        public async Task StopListener()
        {
            await _busControl.StopAsync();
        }


        public async Task Initialize(MessageBusSettings settings, Func<ConsumeContext<RoomUpdate>, Task> callback)
        {
            _setting = settings;

            //ReceivedMessage hodler = new ReceivedMessage();

            _busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(_setting.Host, "/", h =>
                {
                    h.Username(_setting.UserName);
                    h.Password(_setting.Password);
                });

                cfg.ReceiveEndpoint(_setting.Queue, e =>
                {
                    e.Consumer(() => (IConsumer<RoomUpdate>)callback.Target);
                });
            });

            var source = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            await _busControl.StartAsync(source.Token);
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
