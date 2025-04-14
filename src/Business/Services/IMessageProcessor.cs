namespace Business.Services
{
    public interface IMessageProcessor
    {
        Task ProcessMessageAsync(byte[] rawData); 
    }
}