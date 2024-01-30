﻿using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ECommerce.Services.MessageBus.RabbitMQ
{
    public class RabbitMQAuthMessageSender : IRabbitMQAuthMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _username;
        private IConnection _connection;
        public RabbitMQAuthMessageSender()
        {
            _hostName = "localhost";
            _username = "guest";
            _password = "guest";
        }
        public void SendMessage(object message, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                Password = _password,
                UserName = _username,
            };

            _connection = factory.CreateConnection();
            var channel = _connection.CreateChannel();
            channel.QueueDeclare(queueName, false, false, false, null);
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: queueName, body: body);
        }

    }
}
