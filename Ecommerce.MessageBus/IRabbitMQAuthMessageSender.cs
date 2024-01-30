namespace ECommerce.Services.MessageBus
{
    public interface IRabbitMQAuthMessageSender
    {
       void SendMessage(object message, string queueName);
    }
}
