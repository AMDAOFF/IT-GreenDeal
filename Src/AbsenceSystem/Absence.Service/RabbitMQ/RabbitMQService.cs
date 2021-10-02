//using Microsoft.Extensions.Hosting;
//using RabbitMQ.Client;
//using System.Threading;
//using RabbitMQ.Client.Events;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Logging;

//namespace Absence.Service.RabbitMQ
//{
//    public class RabbitMQService : BackgroundService
//    {
//        private readonly ILogger _logger;
//        private IConnection _connection;
//        private IModel _channel;

//        public RabbitMQService(ILoggerFactory loggerFactory)
//        {
//            this._logger = loggerFactory.CreateLogger<RabbitMQService>();
//            InitRabbitMQ();
//        }

//        private void InitRabbitMQ()
//        {
//            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest", VirtualHost = "/" };

//            // create connection  
//            _connection = factory.CreateConnection();

//            // create channel  
//            _channel = _connection.CreateModel();

//            _channel.QueueDeclare("Absence", true, false, false, null);
//            _channel.QueueBind("Absense", null, "Absence");
//            _channel.BasicQos(0, 1, false);

//            _connection.ConnectionShutdown += RabbitMQConnectionShutdown;
//        }

//        protected override Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            stoppingToken.ThrowIfCancellationRequested();

//            var consumer = new EventingBasicConsumer(_channel);
//            consumer.Received += (ch, ea) =>
//            {
//            // received message  
//            var content = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());

//            // handle the received message  
//            HandleMessage(content);
//                _channel.BasicAck(ea.DeliveryTag, false);
//            };

//            consumer.Shutdown += OnConsumerShutdown;
//            consumer.Registered += OnConsumerRegistered;
//            consumer.Unregistered += OnConsumerUnregistered;
//            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

//            _channel.BasicConsume("demo.queue.log", false, consumer);
//            return Task.CompletedTask;
//        }

//        private void HandleMessage(string content)
//        {
//            // we just print this message   
//            _logger.LogInformation($"consumer received {content}");
//        }

//        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
//        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
//        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
//        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
//        private void RabbitMQConnectionShutdown(object sender, ShutdownEventArgs e) { }

//        public override void Dispose()
//        {
//            _channel.Close();
//            _connection.Close();
//            base.Dispose();
//        }
//    }
//}
