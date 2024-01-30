
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ECommerce.Services.MessageBus
{
    public class RabbitMQAuthMessageSender : IRabbitMQAuthMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;
        RabbitMQAuthMessageSender()
        {
            this._hostName = "localhost";
            this._username = "guest";
            this._password = "guest";
        }
        public void SendMessage(object message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = this._hostName,
                Password = this._password,
                UserName = this._username,
            };

            _connection = factory.CreateConnection();
            using var channel = _connection.CreateChannel();
            channel.QueueDeclare(queueName);
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: queueName, body: body);
        }
        
    }
}
