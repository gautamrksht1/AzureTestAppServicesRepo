namespace ECommerce.Services.MessageBus.RabbitMQ
{
    public interface IRabbitMQAuthMessageSender
    {
        void SendMessage(object message, string queueName);
    }
}
