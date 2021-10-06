using Energi.Service.MessageService.DTO;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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
        private MessageBusSettings _settings;               
        public CancellationToken RabbitMQToken { get; set; }
        Func<RoomUpdate, Task> messageCallback;

        public async Task SendMessage(PublishMessageDTO message)
        {
            //await _busControl.Publish(new RoomUpdate(message.RoomNr, message.PeopleCount, message.TimeStamp));
        }

        public async Task StopListener()
        {            
            //await _busControl.StopAsync();
        }


        public async Task Initialize(MessageBusSettings settings, Func<RoomUpdate, Task> callback)
        {
            _settings = settings;

            messageCallback = callback;

            Task rabbitMQTask = Task.Run(() => RunRabbitMQ(settings));
        }

        private Task RunRabbitMQ(MessageBusSettings settings)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = settings.Host, UserName = settings.UserName, Password = settings.Password, VirtualHost = "/" };


            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(settings.Queue, true, false, false);
                EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
                consumer.Received += ConsumerReceived;
                channel.BasicConsume(queue: settings.Queue, autoAck: true, consumer: consumer);
                while (true)
                {
                    if (RabbitMQToken.IsCancellationRequested)
                    {
                        consumer.Received -= ConsumerReceived;
                        break;
                    }
                }
            }
            return null;
        }

        private async void ConsumerReceived(object sender, BasicDeliverEventArgs ea)
        {
            byte[] body = ea.Body.ToArray();
            List<string> strList = Encoding.UTF8.GetString(body).Split(';').ToList();

            // This is a simple check for the POC.
            if (strList.Count == 3)
            {                
                RoomUpdate update = new RoomUpdate(strList[0], Convert.ToInt32(strList[1]), Convert.ToDateTime(strList[2]));

                messageCallback.Invoke(update);
            }

            return;
        }
    }
}