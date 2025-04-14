using Data.Entities;
using Data.Repositories.Implementations;
using Npgsql;
using Dapper;

namespace Data.Tests;

public class MessageRepositoryTests : IDisposable
{
    private readonly string _connectionString;
    private readonly NpgsqlConnection _dbConnection;
    private readonly MessageRepository _repository;

    public MessageRepositoryTests()
    {
        _connectionString = "Host=localhost;Database=NatsMessagesDB;Username=postgres;Password=1234";
        
        _dbConnection = new NpgsqlConnection(_connectionString);
        _dbConnection.Open();

        _dbConnection.Execute(@"
            CREATE TABLE IF NOT EXISTS messages (
                content TEXT,
                receive_date TIMESTAMPTZ
            )");
        
        _dbConnection.Execute("TRUNCATE TABLE messages");
        
        _repository = new MessageRepository(_connectionString);
    }

    public void Dispose()
    {
        _dbConnection.Close();
        _dbConnection.Dispose();
    }

    [Fact]
    public async Task SaveMessageAsync_InsertsMessageCorrectly()
    {
        var testMessage = new Message 
        { 
            Content = "Test message", 
            ReceivedAt = DateTime.UtcNow 
        };

        await _repository.SaveMessageAsync(testMessage);

        var savedMessage = await _dbConnection.QueryFirstOrDefaultAsync<Message>(
            "SELECT content AS Content, receive_date AS ReceivedAt FROM messages");
        
        Assert.NotNull(savedMessage);
        Assert.Equal(testMessage.Content, savedMessage.Content);
        Assert.Equal(testMessage.ReceivedAt, savedMessage.ReceivedAt, TimeSpan.FromSeconds(1));
    }
}