using Data.Entities;
using Npgsql;

namespace Data.Repositories.Implementations
{
    public class MessageRepository : IMessageRepository
    {
        private readonly string _connectionString;

        public MessageRepository(string connectionString)
            => _connectionString = connectionString;

        public async Task SaveMessageAsync(Message message)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new NpgsqlCommand(
                "INSERT INTO messages (content, receive_date) VALUES (@content, @receivedAt)",
                conn
            );
            cmd.Parameters.AddWithValue("content", message.Content);
            cmd.Parameters.AddWithValue("receivedAt", message.ReceivedAt);

            await cmd.ExecuteNonQueryAsync();
            Console.WriteLine($"[DATA] Saved message: '{message.Content}'");
        }
    }
}