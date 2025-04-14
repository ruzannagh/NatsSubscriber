using NATS.Client;

namespace API.Services
{
    public class NatsSubscriberService : IDisposable
    {
        private readonly IConnection _connection;
        private readonly Func<byte[], Task> _onMessageReceived;  

        public NatsSubscriberService(Func<byte[], Task> onMessageReceived)
        {
            _onMessageReceived = onMessageReceived;
            _connection = new ConnectionFactory().CreateConnection();
        }

        public void Subscribe(string subject = "messages")
        {
            _connection.SubscribeAsync(subject, async (_, args) => 
            {
                await _onMessageReceived(args.Message.Data);  
            });
        }

        public void Dispose() => _connection?.Dispose();
    }
}