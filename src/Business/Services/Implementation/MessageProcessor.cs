using Data.Entities; 
using Data.Repositories;
using System.Text;

namespace Business.Services.Implementations
{
    public class MessageProcessor : IMessageProcessor
    {
        private readonly IMessageRepository _repository;

        public MessageProcessor(IMessageRepository repository)
            => _repository = repository;

        public async Task ProcessMessageAsync(byte[] rawData)
        {
            string content = Encoding.UTF8.GetString(rawData).Trim();

            if (string.IsNullOrWhiteSpace(content))
            {
                Console.WriteLine("[BUSINESS] Empty message.");
                return;
            }

            var message = new Message 
            {
                Content = content,
                ReceivedAt = DateTime.UtcNow
            };

            Console.WriteLine($"[BUSINESS] Processed message: '{message.Content}'");
            await _repository.SaveMessageAsync(message);
        }
    }
}