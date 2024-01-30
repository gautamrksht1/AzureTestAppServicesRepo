
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ECommerce.Services.EmailAPI.Services
{
    public class RabbitMQConsumer : BackgroundService
    {

        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private IChannel _channel;
        private readonly EmailService emailService;

        public RabbitMQConsumer(IConfiguration configuration, EmailService emailService)
        {
            _configuration = configuration;
            _connection = createConnection();
            _channel = _connection.CreateChannel();
            _channel.QueueDeclare(_configuration.GetValue<string>("QueueNames:NewUserRegistered"), false, false, false, null);
            this.emailService = emailService;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (channel, evnt) =>
            {
                var content = Encoding.UTF8.GetString(evnt.Body.ToArray());
                string email = JsonSerializer.Deserialize<string>(content);
                HandleMessage(email).GetAwaiter().GetResult();
                _channel.BasicAck(evnt.DeliveryTag, false);
            };

            _channel.BasicConsume(_configuration.GetValue<string>("QueueNames:NewUserRegistered"), false, consumer);

            return Task.CompletedTask;
        }

        private IConnection createConnection()
        {
            if (isConnectionExists())
            {
                return _connection;
            }
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
            };

            return factory.CreateConnection();
        }

        private bool isConnectionExists()
        {
            if (_connection == null)
            {
                return false;
            }

            return true;

        }
        private async Task HandleMessage(string email)
        {
            emailService.RegisterUserEmailAndLog(email).GetAwaiter().GetResult();
        }
    }
}
