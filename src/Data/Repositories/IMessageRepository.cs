using Data.Entities;

namespace Data.Repositories
{
    public interface IMessageRepository
    {
        Task SaveMessageAsync(Message message);
    }
}