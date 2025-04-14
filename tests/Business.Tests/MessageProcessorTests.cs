using Business.Services.Implementations;
using Data.Repositories;
using Moq;
using System.Text;

namespace Business.Tests;

public class MessageProcessorTests
{
    private readonly Mock<IMessageRepository> _mockRepo;
    private readonly MessageProcessor _processor;

    public MessageProcessorTests()
    {
        _mockRepo = new Mock<IMessageRepository>();
        _processor = new MessageProcessor(_mockRepo.Object);
    }

    [Fact]
    public async Task ProcessMessageAsync_ValidMessage_CallsRepository()
    {
        var testMessage = Encoding.UTF8.GetBytes("valid message");
        await _processor.ProcessMessageAsync(testMessage);
        _mockRepo.Verify(r => r.SaveMessageAsync(
            It.Is<Data.Entities.Message>(m => m.Content == "valid message")),
            Times.Once);
    }

    [Theory]
    [InlineData("empty", new byte[0])]
    [InlineData("whitespace", new byte[] {32, 32, 32})]
    public async Task ProcessMessageAsync_InvalidMessages_SkipsRepository(string _, byte[] invalidMessage)
    {
        await _processor.ProcessMessageAsync(invalidMessage);
        _mockRepo.Verify(r => r.SaveMessageAsync(It.IsAny<Data.Entities.Message>()), Times.Never);
    }

    [Fact]
    public async Task ProcessMessageAsync_NullInput_SkipsRepository()
    {
        await _processor.ProcessMessageAsync(null!);
        _mockRepo.Verify(r => r.SaveMessageAsync(It.IsAny<Data.Entities.Message>()), Times.Never);
    }
}