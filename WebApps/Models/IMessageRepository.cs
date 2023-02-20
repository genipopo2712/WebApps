namespace WebApps.Models
{
    public interface IMessageRepository
    {
        IEnumerable<Message> GetMessages();
        int Add(Message obj);
    }
}
